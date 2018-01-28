using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmmunitionBehavior : AmmunitionBehavior {
	void Start()
	{
		damage = 50;
		base.Start();
	}
}
