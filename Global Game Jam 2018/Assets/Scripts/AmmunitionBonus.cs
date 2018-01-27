using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmunitionBonus : MonoBehaviour
{
	abstract public void ApplyBonus (PlayerController player);
	abstract public void ApplyMalus (PlayerController player);
}
