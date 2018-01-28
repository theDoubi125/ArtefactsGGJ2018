using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float MatchDuration;
    public Transform victoryMatchResultMenu;
    public Transform defeatMatchResultMenu;
    private float MatchTime;

    void Start()
    {
        defeatMatchResultMenu.gameObject.SetActive(false);
    }

    public float GetRemainingTime()
    {
        return MatchDuration - MatchTime;
    }

    void Update()
    {
        MatchTime += Time.deltaTime;
        if (MatchTime >= MatchDuration)
        {
            GameController.instance.PauseGame();
            defeatMatchResultMenu.gameObject.SetActive(true);
        }
    }
}
