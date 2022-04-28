using UnityEngine;

public class blinking : MonoBehaviour
{
    private bool lightOn = true;

    private float timeSinceLastSwitch = 0;

    private float timeToUpdate;

    private Light myLight;

    public float maxIntervalOn = 1;
    public float minIntervalOn;

    public float maxIntervalOff = 20;

    public float minIntervalOff = 10;
    // Start is called before the first frame update
    void Start()
    {
        timeToUpdate = Random.Range(minIntervalOff, maxIntervalOff);
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSwitch += Time.deltaTime;
        if (timeToUpdate < timeSinceLastSwitch)
        {
            lightOn = !lightOn;
            timeSinceLastSwitch = 0;
            timeToUpdate = Random.Range(lightOn ? minIntervalOff : minIntervalOn, lightOn ? maxIntervalOff : maxIntervalOn);
        }

        myLight.intensity = lightOn ? 1 : 0;
    }
}
