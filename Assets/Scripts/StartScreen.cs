using UnityEngine;

public class StartScreen : MonoBehaviour
{
  public GameObject bootScreen;
  public GameObject instructions;
  public GameObject startScreen;

  public GameObject gameScreen;

  public GameObject initialScreen;

  public AudioSource pcFans;

  private float timer;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame

  void Update()
  {
    switch (timer)
    {
      case > 10:
        bootScreen.SetActive(false);
        startScreen.SetActive(true);
        timer = -1;
        break;
      case > -1:
        timer += Time.deltaTime;
        pcFans.pitch += Time.deltaTime / 10;
        // print("Fan pitch: " + pcFans.pitch);
        break;
    }

    if (Input.GetKeyDown(KeyCode.Return) && !(instructions.activeSelf))
    {
      instructions.SetActive(true);
      startScreen.SetActive(false);
    }
    else if (Input.GetKeyDown(KeyCode.Return))
    {
      instructions.SetActive(false);
      gameScreen.SetActive(true);
      initialScreen.SetActive(false);
    }
  }
}