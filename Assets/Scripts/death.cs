using UnityEngine;

public class death : MonoBehaviour
{

    public Animator zuccAnimator;

    public Animator doorAnimator;
    
    public float maxIntervalOn = 60;
    public float minIntervalOn = 30;

    public float maxIntervalOff = 10;
    public float minIntervalOff = 0;
    
    private bool zuccEntered;
    private float timeSinceLastSwitch;
    private float timeToUpdate;
    private bool animationPlayed = true;
    private static readonly int DoorOpen = Animator.StringToHash("doorOpened");
    private static readonly int ZuccEntered = Animator.StringToHash("zuccEntered");
    // Start is called before the first frame update
    void Start()
    {
        timeToUpdate = Random.Range(minIntervalOn, maxIntervalOn);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSwitch += Time.deltaTime;
        if (timeToUpdate < timeSinceLastSwitch)
        {
            zuccEntered = !zuccEntered;
            timeSinceLastSwitch = 0;
            timeToUpdate = Random.Range(zuccEntered ? minIntervalOff : minIntervalOn, zuccEntered ? maxIntervalOff : maxIntervalOn);
            animationPlayed = false;
        }
        
        if (animationPlayed) return;
        if (zuccAnimator) zuccAnimator.SetBool(ZuccEntered, zuccEntered);
        if (doorAnimator) doorAnimator.SetBool(DoorOpen, zuccEntered);
        animationPlayed = true;
    }
}
