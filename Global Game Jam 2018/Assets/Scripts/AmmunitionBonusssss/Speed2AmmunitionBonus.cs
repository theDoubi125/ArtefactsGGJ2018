using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed2AmmunitionBonus : AmmunitionBonus
{
	public int BonusFactor = 2;
	public int MalusFactor = -2;

	override public void ApplyBonus(PlayerController player)
	{
		player.acceleration += BonusFactor * Time.deltaTime;
	}

	override public void ApplyMalus(PlayerController player)
	{
		player.acceleration -= MalusFactor * Time.deltaTime;
	}

	override public void StopAction(PlayerController player)
	{
		player.acceleration = player.defaultAcceleration;
	}
}
