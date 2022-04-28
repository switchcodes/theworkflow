using System;
using UnityEngine;

public class ClockForHour : MonoBehaviour
{
    private string oldMinutes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        string minutes = DateTime.Now.ToString("mm");
        if (minutes != oldMinutes)
        {
            updateTimer();
        }

        oldMinutes = minutes; 

    }

    void updateTimer()
    {
        int secondsInt = int.Parse(DateTime.Now.ToString("ss"));
        int minutesInt = int.Parse(DateTime.Now.ToString("mm"));


        transform.Rotate(6,0,0);
        
    }
}
