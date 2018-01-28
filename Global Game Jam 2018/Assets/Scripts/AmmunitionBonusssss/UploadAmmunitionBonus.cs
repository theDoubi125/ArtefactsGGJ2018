using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadAmmunitionBonus : AmmunitionBonus
{
	public int BonusFactor = 2;
	public int MalusFactor = -2;

	override public void ApplyBonus (PlayerController player)
	{
		FindObjectOfType<Transmitter>().bonusFactor[player.playerIndex] = BonusFactor;
	}

	override public void ApplyMalus (PlayerController player)
	{
		FindObjectOfType<Transmitter>().bonusFactor[player.playerIndex] = MalusFactor;
	}

	override public void StopAction (PlayerController player)
	{
		FindObjectOfType<Transmitter>().bonusFactor[player.playerIndex] = 1;
	}
}
