using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    private int currentMathExResult;
    private int points;
    public int scene;
    bool insidePacmanGame = false;
    bool exerciseLoaded = true;
    [SerializeField]GameObject pacmanScreen;
    [SerializeField] GameObject workScreen;
    [SerializeField] GameObject workTab;
    [SerializeField] GameObject gameTab;
    Color takenColor;
    [SerializeField]Color notTakenColor;
    [SerializeField]TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI mathText;
    [SerializeField]TextMeshProUGUI inputField;
    [SerializeField] GameObject inputFieldParent;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Animator workScreenAnimator;

    List<GameObject> coins;
    List<GameObject> enemyMoveChanger;

    [SerializeField] GameObject loadingText;
    [SerializeField] GameObject mathExercse;
    public int timeToAddAfterSuccess = 10;
    public int timeToSubtractAfterFail = -4;
    public int loadingTaskTime = 5;

    float currentTimer;
    [SerializeField] TextMeshProUGUI timerText;
    float currentLoadingTimer;

    public int currentDifficutly = 0;
    public GameObject[] enemyArray;
    public Vector2[] enemyVelocityArray;
    public GameObject pacman;
    public GameObject powerUp;
    public int currentAdditionalDifficulty = 0;

    public AudioSource pacmanSong;

    public GameObject zucc;
    public AudioSource zuccVoice;

    public Animator mainCameraAnimator;
    public Animator zuccAnimator;
    public GameObject gameScreen;


    // Start is called before the first frame update
    void Start()
    {
        takenColor = workTab.GetComponent<SpriteRenderer>().color;
        coins = new List<GameObject>();
        enemyMoveChanger = new List<GameObject>();
        enemyVelocityArray = new Vector2[4];
        
        createNewMathExercise();
        currentTimer = 40;
        loadingText.SetActive(false);
        resetFocus();
    }

    // Update is called once per frame
    void Update() {
	    if (!gameScreen.activeSelf) return;
        if(currentTimer < 0)
        {
            zuccAnimator.Play("zucc_lose");
            currentTimer = 0;
            timerText.text = "Timer:\n" + currentTimer.ToString("0.00");
            return;
        }
        else if(currentTimer == 0)
        {
            return;
        }
        else if (currentTimer < 15)
        {
            timerText.color = Color.red;
        }

        updateTimer();
        
        currentLoadingTimer -= Time.deltaTime;
        if(!exerciseLoaded && currentLoadingTimer <= 0)
        {
            loadNextExercise();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            switchScreen();
        }

        if (!insidePacmanGame && exerciseLoaded==true && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            checkInput();
        }

        if(points >= 100 && currentDifficutly == 0)
        {
            currentDifficutly = 1;
            initialiseNewPacManLevel();
        }
        else if(points >= 175 && currentDifficutly == 1)
        {
            currentDifficutly = 2;
            initialiseNewPacManLevel();
        }
        else if(points >= 225 && currentDifficutly == 2)
        {
            currentDifficutly = 3;
            initialiseNewPacManLevel();
            currentAdditionalDifficulty = 1;
        }
        else if(points >= 225 + (100*currentAdditionalDifficulty) && currentDifficutly == 3)
        {
            currentAdditionalDifficulty++;
            initialiseNewPacManLevel();
        } 
        

        checkForZucc();
    }

    void switchScreen()
    {
        if (insidePacmanGame)
        {
            saveOldEnemyVelocities();
            pacmanScreen.SetActive(false);
            workScreen.SetActive(true);
            gameTab.GetComponent<SpriteRenderer>().color = takenColor;
            workTab.GetComponent<SpriteRenderer>().color = notTakenColor;
            insidePacmanGame = false;
            inputFieldParent.GetComponent<TMP_InputField>().text = "";
            resetFocus();
            pacmanSong.volume = 0;
        }
        else
        {
            restoreOldVelocities();
            pacmanScreen.SetActive(true);
            workScreen.SetActive(false);
            gameTab.GetComponent<SpriteRenderer>().color = notTakenColor;
            workTab.GetComponent<SpriteRenderer>().color = takenColor;
            insidePacmanGame = true;
            restoreOldVelocities();
            pacmanSong.volume = 0.03f;
            if (coins.Count <= 0)
            {
                activateCoins();
            }
        }
    }
    
    public void addPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        //UpdatePointsUI
        pointText.text = "Points: "+points;
    }

    void saveOldEnemyVelocities()
    {
        for(int i = 0; i< enemyArray.Length;i++)
        {
            //Debug.Log(enemyArray[i].GetComponent<Rigidbody2D>().velocity);
            enemyVelocityArray[i] = enemyArray[i].GetComponent<Rigidbody2D>().velocity;
        }
    }
    void restoreOldVelocities()
    {
        for (int i = 0; i < enemyArray.Length; i++)
        {
            enemyArray[i].GetComponent<Rigidbody2D>().velocity = enemyVelocityArray[i];
        }
    }

    void checkInput()
    {
        string input = inputFieldParent.GetComponent<TMP_InputField>().text;
        if (input.Equals("raccoon") && points >= 100)
        {
            zuccAnimator.Play("zucc_win");
            mathExercse.SetActive(false);
            loadingText.SetActive(true);
            loadingText.GetComponent<TextMeshProUGUI>().SetText("YOU WIN!!!");
            currentTimer = 0;
        }
        else if (int.TryParse(input, out int result))
        {
            
            if(result == currentMathExResult)
            {
                addTimer(timeToAddAfterSuccess);
                startLoadingTimer();
                inputFieldParent.GetComponent<TMP_InputField>().text = "";
            }
            else
            {
                addTimer(timeToSubtractAfterFail);
                inputFieldParent.GetComponent<TMP_InputField>().text = "";
                resetFocus();
                //play red animation
                workScreenAnimator.SetTrigger("blinkRed");
            }
        }
        else
        {
            inputFieldParent.GetComponent<TMP_InputField>().text = "";
            workScreenAnimator.SetTrigger("blinkRed");
            resetFocus();
        }
        
    }

    void createNewMathExercise()
    {
        int chooseRandomExerciseType = Random.Range(0, 3);
        string mathString = "";
        if(chooseRandomExerciseType == 0)
        {
            int random1 = Random.Range(1, (int)currentTimer);
            int random2 = Random.Range(1, (int)currentTimer);
            currentMathExResult = (random1 + random2);
            mathString = random1 + " + " + random2 + " = ";
        }
        else if (chooseRandomExerciseType == 1)
        {
            int random1 = Random.Range(1, 11);
            int random2 = Random.Range(1, (int)currentTimer/2);
            currentMathExResult = (random1 * random2);
            mathString = random1 + " * " + random2 + " = ";
        }
        else if (chooseRandomExerciseType == 2)
        {
            int random1 = Random.Range(1, 11);
            int random2 = Random.Range(1, 11);
            int random3 = Random.Range(1, (int)currentTimer);
            currentMathExResult = ((random1 * random2) + random3);
            mathString = "(" +random1 + " * " + random2 + ") + " + random3 + " = ";
        }
        mathText.SetText(mathString);
        resetFocus();
    }

    public int getPoints()
    {
        return points;
    }
    private void resetFocus()
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(inputField.transform.parent.parent.gameObject);
    }
    private void addTimer(int timeToAdd)
    {
        currentTimer += timeToAdd;
        timerText.text = "Timer:\n" + currentTimer.ToString("0.00");
        if(currentTimer >= 15)
        {
            timerText.color = Color.black;
        }
    }
    private void updateTimer()
    {
        currentTimer -= Time.deltaTime;
        timerText.text = "Timer:\n" + currentTimer.ToString("0.00");
        
    }
    private void startLoadingTimer()
    {
        exerciseLoaded = false;
        mathExercse.SetActive(false);
        currentLoadingTimer = loadingTaskTime;
        loadingText.SetActive(true);
    }
    private void loadNextExercise()
    {
        exerciseLoaded = true;
        loadingText.SetActive(false);
        mathExercse.SetActive(true);
        createNewMathExercise();
    }
    private void activateCoins()
    {
        if(coins.Count <= 0)
        {
            foreach (GameObject coin in GameObject.FindGameObjectsWithTag("coin_trigger"))
            {
                coins.Add(coin);
            }
        }
        
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
    }
    private void initialiseNewPacManLevel()
    {
        activateCoins();

        if(enemyMoveChanger.Count <= 0)
        {
            foreach (GameObject moveChanger in GameObject.FindGameObjectsWithTag("EnemyMoveChanger"))
            {
                enemyMoveChanger.Add(moveChanger);
            }
        }

        int randomPositionIndex = Random.Range(0, enemyMoveChanger.Count);
        pacman.SetActive(true);
        pacman.transform.position = enemyMoveChanger[randomPositionIndex].transform.position;

        for(int i = 0; i < enemyArray.Length;i++)
        {
            enemyArray[i].SetActive(false);
        }

        for(int i = 0; i <= currentDifficutly; i++)
        {
            enemyArray[i].SetActive(true);
            int secondRandomIndex = Random.Range(0, enemyMoveChanger.Count);
            while (secondRandomIndex == randomPositionIndex)
            {
                secondRandomIndex = Random.Range(0, enemyMoveChanger.Count);

            }
            enemyArray[i].transform.position = enemyMoveChanger[secondRandomIndex].transform.position;
        }
        powerUp.SetActive(true);
        powerUp.transform.position = enemyMoveChanger[Random.Range(0, enemyMoveChanger.Count)].transform.position;
        
        
    }
    public void pacmanDies()
    {
        currentAdditionalDifficulty = 0;
        points = 0;
        currentDifficutly = 0;
        initialiseNewPacManLevel();
    }

    void checkForZucc() {
	    if (!(zucc.transform.position.x > -240f) || !insidePacmanGame) return;
	    mainCameraAnimator.Play("camera_knockout");
    }
}
