using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health2AmmunitionBonus : AmmunitionBonus
{
	public int BonusFactor = 6;
	public int MalusFactor = -6;
	private bool bonusGiven = false;

	override public void ApplyBonus(PlayerController player)
	{
		if (!bonusGiven)
		{
			player.GetComponent<HealthController>().currentHP += BonusFactor;
			bonusGiven = true;
		}
	}

	override public void ApplyMalus(PlayerController player)
	{
		if (!bonusGiven)
		{
			player.GetComponent<HealthController>().currentHP += MalusFactor;
			bonusGiven = true;
		}
	}

	override public void StopAction(PlayerController player)
	{
		bonusGiven = false;
	}
}
