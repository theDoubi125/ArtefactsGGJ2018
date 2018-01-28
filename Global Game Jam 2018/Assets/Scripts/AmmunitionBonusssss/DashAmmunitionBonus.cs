using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAmmunitionBonus : AmmunitionBonus
{
	public int BonusFactor = -1;
	public int MalusFactor = 1;

	override public void ApplyBonus (PlayerController player)
	{
		player.GetComponent<ActionController> ().dashReloadBonusFactor = BonusFactor;
	}

	override public void ApplyMalus (PlayerController player)
	{
		player.GetComponent<ActionController> ().dashReloadBonusFactor = MalusFactor;
	}

	override public void StopAction (PlayerController player)
	{
		player.GetComponent<ActionController> ().dashReloadBonusFactor = 0;
	}
}
