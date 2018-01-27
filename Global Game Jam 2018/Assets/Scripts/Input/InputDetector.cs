using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputDetector : MonoBehaviour
{
    public List<string> inputsToCheck;
    InputMenuDisplay inputDisplay;
    public float timerDuration = 5;
    private float timerTime = 0;
    bool playTimer = false;
    public string gameSceneName;
    private Text text; 

    void Start()
    {
        inputDisplay = GetComponentInChildren<InputMenuDisplay>();
        text = GetComponentInChildren<Text>();
        text.enabled = false;
    }

    void Update()
    {
        
        if (playTimer)
            timerTime += Time.deltaTime;
        text.text = "Start in " + (timerDuration - timerTime) + "s";
        if (timerTime > timerDuration)
        {
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
        for (int i = 0; i < 5; i++)
        {
            string InputPrefix = GameController.instance.GetPlayerControllerInputPrefix(i);
            for(int j=0; j<inputsToCheck.Count; j++)
            {
                if(Input.GetButtonDown(InputPrefix + inputsToCheck[j]))
                {
                    if (GameController.instance.registerPlayer(i))
                    {
                        inputDisplay.AddPlayer(i);
                        timerTime = 0;
                        playTimer = true;
                        text.enabled = true;
                    }
                }
            }

        }
    }
}
