using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
	public float maxValue = 100f;
	public float startValue = 0f;
	public float transmissionSpeed = 1f;
	public float bonusFactor = 1f;
	public bool[] arePlayersChanneling = { false, false, false, false };
	public Vector3[] gaugesInitialPositions;
	public Vector3[] gaugesFinalPositions;
	public float[] playerScores = {0f,0f,0f,0f};

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < 4; i++) {
			if (arePlayersChanneling [i]) {
				playerScores [i] += Time.deltaTime * transmissionSpeed * bonusFactor;

			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		ActionController tmp = col.GetComponent<ActionController> ();
		if (tmp != null)
		{
			//TODO : Ajouter variables dans ActionController
			//tmp.isInRangeOfTransmitter = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		ActionController tmp = col.GetComponent<ActionController> ();
		if (tmp != null)
		{
			//TODO : Ajouter variables dans ActionController
			//tmp.isInRangeOfTransmitter = false;
		}
	}
}
