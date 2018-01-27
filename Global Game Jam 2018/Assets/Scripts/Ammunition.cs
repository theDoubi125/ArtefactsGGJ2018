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
//	[HideInInspector]
	public AmmunitionBehavior behavior;
//	[HideInInspector]
	public AmmunitionBonus bonus;
	public PlayerController shooter;
	//TODO : Add graph variables
	public Transform billboardApparence;

	void Awake()
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
				if (GetComponent<Rigidbody>() != null)
				{
					ChangeToBillboard();
				}
				GetComponent<AmmunitionBonus>().ApplyMalus(shooter);
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

	void ChangeToBillboard()
	{
		DestroyObject(GetComponent<Rigidbody>());
		GetComponent<Collider>().isTrigger = true;
		//TODO : Change apparence
	}

	void OnTriggerEnter (Collider col)
	{
		PlayerController tmp = col.GetComponent<PlayerController>();
		if (tmp != null)
		{
			tmp.GetComponent<WeaponController>().HarvestCrate(this);
			//TODO : Play SFX
			Destroy(gameObject);
		}
	}
}
