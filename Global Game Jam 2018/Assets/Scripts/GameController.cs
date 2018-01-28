using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<int> controllers;

	// Use this for initialization
	void Awake ()
	{
        if (instance == null)
            instance = this;
        else
            gameObject.SetActive(false);
	}

	void Start ()
	{
        DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

    public bool IsPlayerControlled(int playerIndex)
    {
        return controllers.Count > playerIndex;
    }

    public string GetPlayerControllerInputPrefix(int controllerIndex)
    {
        if (controllerIndex > 0)
            return "Player" + controllerIndex + "_";
        else
            return "Keyboard" + "_";
    }

    public string GetPlayerInputPrefix(int playerIndex)
    {
        if (controllers.Count <= playerIndex)
            return "ERROR_";
        return GetPlayerControllerInputPrefix(controllers[playerIndex]);
    }

    public bool registerPlayer(int playerId)
    {
        for (int i = 0; i < controllers.Count; i++)
        {
            if (controllers[i] == playerId)
                return false;
        }
        controllers.Add(playerId);
        return true;
    }
}