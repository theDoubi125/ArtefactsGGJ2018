using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor2AmmunitionBonus : AmmunitionBonus
{
	public int BonusFactor = 0;
	public int MalusFactor = 2;

	override public void ApplyBonus (PlayerController player)
	{
		player.GetComponent<HealthController> ().bonusFactor = BonusFactor;
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
