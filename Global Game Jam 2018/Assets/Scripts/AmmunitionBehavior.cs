using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBehavior : MonoBehaviour
{
	public PlayerController hitPlayer;
	public int damage;

	// Use this for initialization
	protected void Start()
	{
		if (GetComponent<PlayerController>() == null)
		{
			ApplyBehavior();
		}
	}

	virtual public void ApplyBehavior()
	{
		if (GetComponent<Rigidbody>() != null)
        	GetComponent<Rigidbody>().AddForce(transform.forward*1000);
	}

	virtual public void OnCollisionEnter(Collision col)
	{
		hitPlayer = col.collider.gameObject.GetComponent<PlayerController>();
		//TODO : need to change this to be able to injure oneself by rebound, need to change the origin of the bullet to do so.
		if (hitPlayer == null)
		{
			enabled = false;
		}
		else
		{
			if (hitPlayer.playerIndex != GetComponent<Ammunition>().shooter.playerIndex)
			{
//				Debug.Log(hitPlayer + " | " + hitPlayer.playerIndex + " | " + GetComponent<Ammunition>().PlayerId);
				GetComponent<Ammunition>().ApplyDamage();
			}
		}
	}
}
