using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
	public List<Ammunition> ammunitions;
	public int initialAmmo = 3;
    public List<PlayerController> players;

    public List<int> controllers;

	public List<GameObject> ammoObjects;

	// Use this for initialization
	void Awake ()
	{
        instance = this;
		foreach (Ammunition ammo in ammunitions) {
			GameObject tmp = new GameObject ();
			tmp.AddComponent (ammo.GetType ());
			ammoObjects.Add (tmp);
		}
	}

	void Start ()
	{
        DontDestroyOnLoad(gameObject);
		for (int i = 0; i < initialAmmo; i++) {
			int rdm = Random.Range (0, ammunitions.Count - 1);
			foreach (PlayerController player in players) {
				WeaponController weapon = player.GetComponent<WeaponController> ();
				GameObject tmp = Instantiate (ammoObjects [rdm]);
				tmp.transform.SetParent (player.transform);
				weapon.magazine.Add (tmp);
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		
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