using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transmitter : MonoBehaviour
{
	public float maxValue = 100f;
	public float startValue = 0f;
	public float transmissionSpeed = 1f;
	public float[] bonusFactor = {1f, 1f, 1f, 1f};
	public bool[] arePlayersChanneling = { false, false, false, false };
	public Transform[] gauges;
	public Vector3[] gaugesInitialPositions;
	public Vector3[] gaugesFinalPositions;
	public float[] playerScores = {0f,0f,0f,0f};
	public GameObject winGUI;
	private bool won = false;
	private int animCounter = 0;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < 4; i++) {
			gaugesInitialPositions [i] = gauges [i].position;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!won) {
			animCounter = 0;
			for (int i = 0; i < 4; i++) {
				if (arePlayersChanneling [i]) {
					animCounter++;
					playerScores [i] += Time.deltaTime * transmissionSpeed * bonusFactor [i];
					gauges [i].position = Vector3.Lerp (gaugesInitialPositions [i], gaugesFinalPositions [i], playerScores [i] / maxValue);
					if (playerScores [i] >= maxValue) {
						Win (i);
					}
				}
			}
			if (animCounter > 0 && !GetComponent<Animation> ().isPlaying)
				GetComponent<Animation> ().Play ();
			if (animCounter == 0)
				GetComponent<Animation> ().Stop ();
		}
	}

	void Win(int winner)
	{
		winGUI.SetActive (true);
		PlayerController[] players = FindObjectsOfType<PlayerController> ();
		foreach (PlayerController player in players) {
			if (player.playerIndex == winner) {
				winGUI.transform.GetChild (4).gameObject.GetComponent<Image> ().sprite = player.characterSprite;
			}
		}

	}

	void OnTriggerEnter(Collider col)
	{
		ActionController tmp = col.GetComponent<ActionController> ();
		if (tmp != null)
		{
			tmp.isInRangeOfTransmitter = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		ActionController tmp = col.GetComponent<ActionController> ();
		if (tmp != null)
		{
			tmp.isInRangeOfTransmitter = false;
		}
	}
}
