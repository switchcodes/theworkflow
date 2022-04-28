using System;
using UnityEngine;

public class ClockForMinutes : MonoBehaviour
{
    private string oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        string seconds = DateTime.Now.ToString("ss");
        if (seconds != oldSeconds)
        {
            updateTimer();
        }

        oldSeconds = seconds; 

    }

    void updateTimer()
    {
        int secondsInt = int.Parse(DateTime.Now.ToString("ss"));
        int minutesInt = int.Parse(DateTime.Now.ToString("mm"));
        
        transform.Rotate(6,0,0);
        
    }
}
