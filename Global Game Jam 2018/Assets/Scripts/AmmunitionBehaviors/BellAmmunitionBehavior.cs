using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellAmmunitionBehavior : AmmunitionBehavior{

	new void Start()
	{
		base.Start ();
		damage = 20;
	}
	
	virtual public void ApplyBehavior()
	{
		if (GetComponent<Rigidbody>() != null)
			GetComponent<Rigidbody>().AddForce(new Vector3(1,1,0)*500);
	}
}
