using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundAmmunitionBehavior : AmmunitionBehavior {

	private int nbRebounds = 3;

	new void Start()
	{
		base.Start ();
		damage = 15;
	}

	virtual public void ApplyBehavior()
	{
		if (GetComponent<Rigidbody>() != null)
			GetComponent<Rigidbody>().AddForce(transform.forward*2000);
	}

	virtual public void OnCollisionEnter(Collision col)
	{
		hitPlayer = col.collider.gameObject.GetComponent<PlayerController>();
		//TODO : need to change this to be able to injure oneself by rebound, need to change the origin of the bullet to do so.
		if (hitPlayer == null)
		{
			if (nbRebounds > 0) {
				nbRebounds--;
				damage += 5;
			} else {
				Ammunition tmp = GetComponent<Ammunition> ();
				tmp.ChangeToBillboard ();
				tmp.isBillboard = true;
			}
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
