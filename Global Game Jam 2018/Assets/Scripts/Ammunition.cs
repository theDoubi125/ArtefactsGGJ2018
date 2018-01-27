using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
	public enum behaviorEnum
	{
		TestAmmunitionBehavior
	};
	public enum bonusEnum
	{
		TestAmmunitionBonus
	};
	public behaviorEnum behaviorchoice;
	public bonusEnum bonuschoice;
	[HideInInspector]
	public AmmunitionBehavior behavior;
	[HideInInspector]
	public AmmunitionBonus bonus;
	public int PlayerId;
	//TODO : Add graph variables

	void Start()
	{
		switch (behaviorchoice)
		{
			case behaviorEnum.TestAmmunitionBehavior:
				gameObject.AddComponent<TestAmmunitionBehavior>();
				behavior = GetComponent<TestAmmunitionBehavior>();
				break;
			default:
				break;
		}

		switch (bonuschoice)
		{
			case bonusEnum.TestAmmunitionBonus:
				gameObject.AddComponent<TestAmmunitionBonus>();
				bonus = GetComponent<TestAmmunitionBonus>();
				break;
			default:
				break;
		}
	}

	void Update()
	{
		if (!behavior.enabled)
		{
			if (!behavior.hitPlayer)
			{
				ChangeToCrate();
			}
			else
			{
				ApplyDamage();
			}
		}
	}

	void ApplyDamage()
	{
		//TODO : implement HealthController
		//behavior.hitPlayer.gameObject.GetComponent<HealthController>().Hit(this);
		behavior.hitPlayer.gameObject.GetComponent<WeaponController>().Hit();
	}

	void ChangeToCrate()
	{
		bonus.ApplyMalus();
	}
}
