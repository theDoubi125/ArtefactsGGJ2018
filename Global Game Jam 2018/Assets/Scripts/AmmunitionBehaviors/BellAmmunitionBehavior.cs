using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellAmmunitionBehavior : AmmunitionBehavior{

	new void Start()
	{
		base.Start ();
		damage = 20;
	}
	
	override public void ApplyBehavior()
	{
		if (GetComponent<Rigidbody>() != null)
			GetComponent<Rigidbody>().AddForce(new Vector3(1,5,0)*50);
	}
}
