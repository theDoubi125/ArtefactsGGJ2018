using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAmmunitionBonus : AmmunitionBonus
{
	public float BonusFactor = 0.5f;
	public float MalusFactor = 1.5f;

	override public void ApplyBonus (PlayerController player)
	{
		player.GetComponent<HealthController>().bonusFactor = BonusFactor;
	}

	override public void ApplyMalus (PlayerController player)
	{
		player.GetComponent<HealthController> ().bonusFactor = MalusFactor;
	}

	override public void StopAction (PlayerController player)
	{
		player.GetComponent<HealthController> ().bonusFactor = 1;
	}
}
