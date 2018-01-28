using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAmmunitionBehavior : AmmunitionBehavior
{
	new void Start ()
	{
		base.Start ();
		damage = 30;
	}
}
