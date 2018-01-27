using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmmunitionBonus : AmmunitionBonus {

	// Use this for initialization
	void Start () {
		
	}

	override public void ApplyBonus (PlayerController player)
	{
		player.acceleration += 1 * Time.deltaTime;
	}

	override public void ApplyMalus (PlayerController player)
	{
		player.acceleration -= 1 * Time.deltaTime;
	}
}
