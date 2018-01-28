﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAmmunitionBehavior : AmmunitionBehavior {

	new void Start()
	{
		base.Start ();
		damage = 30;
	}

	override public void ApplyBehavior()
	{
		if (GetComponent<Rigidbody>() != null)
			GetComponent<Rigidbody>().AddForce(transform.forward*1000);
	}
}