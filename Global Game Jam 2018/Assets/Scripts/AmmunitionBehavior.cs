using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBehavior : MonoBehaviour
{
	public PlayerController hitPlayer;

	// Use this for initialization
	void Start()
	{
		if (GetComponent<PlayerController>() == null)
		{
			ApplyBehavior();
		}
	}

	virtual public void ApplyBehavior()
	{
		GetComponent<Rigidbody>().AddForce(Vector3.forward*1000);
	}

	virtual public void OnCollisionEnter(Collision col)
	{
		hitPlayer = col.collider.gameObject.GetComponent<PlayerController>();
		//TODO : need to change this to be able to injure oneself by rebound, need to change the origin of the bullet to do so.
		if (hitPlayer == null
		    || hitPlayer.playerIndex != GetComponent<Ammunition>().PlayerId)
		{
//			Debug.Log(hitPlayer + " | " + hitPlayer.playerIndex + " | " + GetComponent<Ammunition>().PlayerId);
			this.enabled = false;
		}
	}
}
