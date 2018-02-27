using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEffect : MonoBehaviour
{
    public delegate void TimerDelegate();
    public TimerDelegate OnTimeOver;

    private float timeElapsed = 0;
    public float duration = 3;

    public float remainingTimeRatio { get { return (duration - timeElapsed) / duration; } }

	void Start ()
    {
		
	}
	
	void Update ()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > duration)
        {
            OnTimeOver();
            timeElapsed = 0;
        }
	}
}
