using UnityEngine;

public class Prototype_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocity = 1f;
    public GameManager gm;
    bool huntingMode;
    float currentTimer;
    public float maxTimer;

    Color normalColor;
    SpriteRenderer pacmanSpriteRenderer;
    public Color specialColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pacmanSpriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = pacmanSpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-1, 0) * velocity;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(1, 0)*velocity;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 1)*velocity;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(0, -1)*velocity;
        }
        

        if (huntingMode)
        {
            currentTimer = currentTimer - Time.deltaTime;
            if(currentTimer <= 0)
            {
                huntingMode = false;
                pacmanSpriteRenderer.color = normalColor;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Pacman_Enemy")
        {
            if (huntingMode)
            {
                collision.collider.gameObject.SetActive(false);
                gm.addPoints(5);
            }
            else
            {
                PacmanDies();
            }
           
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "coin_trigger"){
            //Debug.Log("coin collected");
            collider.gameObject.SetActive(false);
            gm.addPoints(1);
            
        }
        if(collider.gameObject.tag == "powerCoin_trigger"){
            // Geister zerstörbar machen
            huntingMode = true;
            //Destroy(collider.gameObject);
            collider.gameObject.SetActive(false);
            currentTimer = maxTimer;
            pacmanSpriteRenderer.color = specialColor;
        }
        
    }

    private void PacmanDies()
    {
        
        gameObject.SetActive(false);
        gm.pacmanDies();
    }
}
