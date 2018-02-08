using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionInitialiser : MonoBehaviour
{
	public int initialAmmo = 3;
	public PlayerController[] players;
	public GameObject[] hudPlayers;
	public Sprite[] behaviorSprites;
	public Sprite[] bonusSprites;
	public Sprite[] lvlSprites;
	public Color[] colorStates;

	public List<GameObject> ammoObjects;

	// Use this for initialization
	void Awake ()
	{
		players = FindObjectsOfType<PlayerController> ();
		foreach (PlayerController player in players) {
			hudPlayers [player.playerIndex].SetActive (true);
			player.personnalHUD = hudPlayers [player.playerIndex];
		}
	}

	void Start ()
	{
	}
}