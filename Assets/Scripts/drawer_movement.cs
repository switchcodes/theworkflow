using UnityEngine;

public class drawer_movement : MonoBehaviour
{

    public Animator animator;

    public float maxIntervalOn = 60;
    public float minIntervalOn = 30;

    public float maxIntervalOff = 10;
    public float minIntervalOff = 0;
    
    private bool drawerOpen;

    private bool animationPlayed = true;

    private float timeSinceLastSwitch;

    private float timeToUpdate;
    private static readonly int Open = Animator.StringToHash("open");

    // Start is called before the first frame update
    void Start()
    {
        timeToUpdate = Random.Range(minIntervalOff, maxIntervalOff);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSwitch += Time.deltaTime;
        if (timeToUpdate < timeSinceLastSwitch)
        {
            drawerOpen = !drawerOpen;
            timeSinceLastSwitch = 0;
            timeToUpdate = Random.Range(drawerOpen ? minIntervalOff : minIntervalOn, drawerOpen ? maxIntervalOff : maxIntervalOn);
            animationPlayed = false;
        }

        if (animationPlayed) return;
        animator.SetBool(Open, drawerOpen);
        animationPlayed = true;
    }
}
