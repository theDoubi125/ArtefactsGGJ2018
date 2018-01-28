using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float MatchDuration;
    public Transform victoryMatchResultMenu;
    public RectTransform defeatMatchResultMenuPrefab;
    public GameTimerDisplay timer;
    private float MatchTime;

    void Start()
    {
        timer = GetComponentInChildren<GameTimerDisplay>();
    }

    public float GetRemainingTime()
    {
        return MatchDuration - MatchTime;
    }

    void Update()
    {
        if (GameController.instance.isGamePaused)
            return;
        if(MatchTime < MatchDuration)
            MatchTime += Time.deltaTime;
        timer.SetTime(GetRemainingTime());
        if (MatchTime >= MatchDuration)
        {
            GameController.instance.PauseGame();
            RectTransform instance = Instantiate<RectTransform>(defeatMatchResultMenuPrefab);
            instance.SetParent(transform, false);
        }
    }
}
