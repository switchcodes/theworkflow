using UnityEngine;

public class pacman_enemy_movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocity = 5f;
    Vector2 oldVelocity;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeVelocity(1, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Vector2 dir = new Vector2();
        if(collider.tag == "EnemyMoveChanger")
        {
            dir = collider.GetComponent<EnemyMoveChanger>().GetNewMoveDirection();
            transform.position = collider.transform.position;
            ChangeVelocity(dir.x, dir.y);
        }

       
    }

    void ChangeVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y) * velocity;
        //Debug.Log(rb.velocity);
    }

    public void saveOldVelocity()
    {
        oldVelocity = rb.velocity;
    }
}
