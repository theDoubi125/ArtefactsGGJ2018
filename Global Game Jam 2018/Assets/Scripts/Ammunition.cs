using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
	public enum behaviorEnum
	{
		Base,
		Bell,
		Rebound,
		Sniper}

	;

	public enum bonusEnum
	{
		Armor2,
		Armor,
		DashReload,
		Health2,
		Health,
		Speed2,
		Speed,
		Upload}

	;

	public behaviorEnum behaviorchoice;
	public bonusEnum bonuschoice;
	[HideInInspector]
	public AmmunitionBehavior behavior;
	[HideInInspector]
	public AmmunitionBonus bonus;
	public PlayerController shooter;
	public bool isBillboard = false;
	//TODO : Add graph variables
	public Sprite bonusSpritePositive;
	public Sprite bonusSpriteNegative;
	public Color colorOwner;
	public Sprite lvlSprite;
	public Sprite behaviorSprite;
	public Transform billboardApparence;

	void Start ()
	{
		switch (behaviorchoice) {
		case behaviorEnum.Base:
			gameObject.AddComponent<BaseAmmunitionBehavior> ();
			behavior = GetComponent<BaseAmmunitionBehavior> ();
			break;
		case behaviorEnum.Bell:
			gameObject.AddComponent<BellAmmunitionBehavior> ();
			behavior = GetComponent<BellAmmunitionBehavior> ();
			break;
		case behaviorEnum.Rebound:
			gameObject.AddComponent<ReboundAmmunitionBehavior> ();
			behavior = GetComponent<ReboundAmmunitionBehavior> ();
			break;
		case behaviorEnum.Sniper:
			gameObject.AddComponent<SniperAmmunitionBehavior> ();
			behavior = GetComponent<SniperAmmunitionBehavior> ();
			break;
		default:
			break;
		}

		switch (bonuschoice) {
		case bonusEnum.Armor2:
			gameObject.AddComponent<Armor2AmmunitionBonus> ();
			bonus = GetComponent<Armor2AmmunitionBonus> ();
			break;
		case bonusEnum.Armor:
			gameObject.AddComponent<ArmorAmmunitionBonus> ();
			bonus = GetComponent<ArmorAmmunitionBonus> ();
			break;
		case bonusEnum.DashReload:
			gameObject.AddComponent<DashAmmunitionBonus> ();
			bonus = GetComponent<DashAmmunitionBonus> ();
			break;
		case bonusEnum.Health2:
			gameObject.AddComponent<Health2AmmunitionBonus> ();
			bonus = GetComponent<Health2AmmunitionBonus> ();
			break;
		case bonusEnum.Health:
			gameObject.AddComponent<HealthAmmunitionBonus> ();
			bonus = GetComponent<HealthAmmunitionBonus> ();
			break;
		case bonusEnum.Speed2:
			gameObject.AddComponent<Speed2AmmunitionBonus> ();
			bonus = GetComponent<Speed2AmmunitionBonus> ();
			break;
		case bonusEnum.Speed:
			gameObject.AddComponent<SpeedAmmunitionBonus> ();
			bonus = GetComponent<SpeedAmmunitionBonus> ();
			break;
		case bonusEnum.Upload:
			gameObject.AddComponent<UploadAmmunitionBonus> ();
			bonus = GetComponent<UploadAmmunitionBonus> ();
			break;
		default:
			break;
		}
	}

	void Update ()
	{
		if (isBillboard)
			GetComponent<AmmunitionBonus> ().ApplyMalus (shooter);
	}

	public void ApplyDamage ()
	{
		behavior.hitPlayer.gameObject.GetComponent<HealthController> ().Hit (this);
		behavior.hitPlayer.gameObject.GetComponent<WeaponController> ().HarvestCrate (this);
	}

	public void ChangeToBillboard ()
	{
		DestroyObject (GetComponent<Rigidbody> ());
		GetComponent<Collider> ().isTrigger = true;
		//TODO : Change apparence
	}

	void OnTriggerEnter (Collider col)
	{
		PlayerController tmp = col.GetComponent<PlayerController> ();
		if (tmp != null) {
			tmp.GetComponent<WeaponController> ().HarvestCrate (this);
			//TODO : Play SFX
			Destroy (gameObject);
		}
	}
}
