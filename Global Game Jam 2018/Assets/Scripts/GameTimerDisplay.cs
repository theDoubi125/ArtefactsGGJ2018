using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerDisplay : MonoBehaviour
{
    private Text timerText;
	
    void Start () 
    {
        timerText = GetComponent<Text>();
	}
	
    public void SetTime (float availableTime)
    {
        timerText.text = "" + Mathf.RoundToInt(availableTime);
	}
}
