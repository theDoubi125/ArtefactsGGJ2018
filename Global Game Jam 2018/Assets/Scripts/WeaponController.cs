using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
	public Transform bullet;
	public List<GameObject> magazine;
	public int maxCapacity = 5;
	public PlayerController player;
	public GameObject hudAmmo;
	public Transform hudAmmoParent;
	public Color Freddygreen;
	public Color Freddyred;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<PlayerController>();
		magazine.Capacity = maxCapacity;
		hudAmmoParent = GetComponent<PlayerController> ().personnalHUD.transform.GetChild (2);
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown(player.GetPlayerInputPrefix() + "Action1"))
		{
			if (magazine.Count > 0)
			{
				CreateProjectile();
			}
			else
			{
				//TODO : Play SFX
			}
		}
		if (magazine.Count > 0)
		{
			magazine[0].GetComponent<AmmunitionBonus>().ApplyBonus(player);
		}
	}

	public void AddHUDAmmo (Ammunition ammo)
	{
		GameObject newHUDAmmo = Instantiate (hudAmmo);
		newHUDAmmo.transform.GetChild(1).gameObject.GetComponent<Image> ().sprite = ammo.bonusSpritePositive;
		newHUDAmmo.transform.GetChild (1).gameObject.GetComponent<Image> ().color = Freddygreen;
		newHUDAmmo.transform.GetChild(3).gameObject.GetComponent<Image> ().sprite = ammo.lvlSprite;
		newHUDAmmo.transform.GetChild (3).gameObject.GetComponent<Image> ().color = Freddygreen;
		newHUDAmmo.transform.GetChild(4).gameObject.GetComponent<Image> ().sprite = ammo.behaviorSprite;
		newHUDAmmo.transform.SetParent (hudAmmoParent, false);
	}

	public void DeleteHUDAmmo()
	{
		Destroy(hudAmmoParent.transform.GetChild(0).gameObject);
	}

	public void StealWeapon(WeaponController victim)
	{
		if (victim.magazine.Count > 0) {
			Transform projectile = Instantiate (bullet);
			projectile.position = victim.transform.position + player.GetWeaponDirection () * 8f;
			projectile.rotation = Quaternion.LookRotation (player.GetWeaponDirection ()); 	
			projectile.gameObject.AddComponent <Ammunition> ();
			Ammunition tmp = projectile.gameObject.GetComponent<Ammunition> ();
			tmp.behaviorchoice = victim.magazine [0].GetComponent<Ammunition> ().behaviorchoice;
			tmp.bonuschoice = victim.magazine [0].GetComponent<Ammunition> ().bonuschoice;
			tmp.shooter = victim.player;
			victim.magazine [0].GetComponent<AmmunitionBonus> ().StopAction (player);
			Destroy (victim.magazine [0]);
			victim.magazine.RemoveAt (0);
			victim.DeleteHUDAmmo ();
			tmp.isBillboard = true;
			tmp.ChangeToBillboard ();
		}
	}

	public void CreateProjectile()
	{
		Transform projectile = Instantiate(bullet);
        projectile.position = transform.position + player.GetWeaponDirection() * 0.5f;
        projectile.rotation = Quaternion.LookRotation(player.GetWeaponDirection()); 	
		projectile.gameObject.AddComponent <Ammunition>();
		Ammunition tmp = projectile.gameObject.GetComponent<Ammunition>();
		tmp.behaviorchoice = magazine[0].GetComponent<Ammunition>().behaviorchoice;
		tmp.bonuschoice = magazine[0].GetComponent<Ammunition>().bonuschoice;
		tmp.behaviorSprite = magazine [0].GetComponent<Ammunition> ().behaviorSprite;
		tmp.bonusSpriteNegative = magazine [0].GetComponent<Ammunition> ().bonusSpriteNegative;
		tmp.bonusSpritePositive= magazine [0].GetComponent<Ammunition> ().bonusSpritePositive;
		tmp.lvlSprite = magazine [0].GetComponent<Ammunition> ().lvlSprite;
		tmp.shooter = player;
		magazine [0].GetComponent<AmmunitionBonus> ().StopAction (player);
		Destroy(magazine[0]);
		magazine.RemoveAt(0);
		DeleteHUDAmmo ();
	}

	public void HarvestCrate (Ammunition ammo)
	{
//		Debug.Log("Crate harvested");
		GameObject tmp = new GameObject();
		tmp.AddComponent<Ammunition>();
		tmp.GetComponent<Ammunition>().behaviorchoice = ammo.behaviorchoice;
		tmp.GetComponent<Ammunition>().bonuschoice = ammo.bonuschoice;
		Debug.Log (ammo.bonusSpriteNegative + " | " + ammo.bonusSpritePositive + " | " + ammo.behaviorSprite);
		tmp.GetComponent<Ammunition> ().bonusSpriteNegative = ammo.bonusSpriteNegative;
		tmp.GetComponent<Ammunition> ().bonusSpritePositive= ammo.bonusSpritePositive;
		tmp.GetComponent<Ammunition> ().behaviorSprite= ammo.behaviorSprite;
		tmp.GetComponent<Ammunition> ().lvlSprite = ammo.lvlSprite;
		tmp.transform.SetParent (transform);
		magazine.Add(tmp);
		AddHUDAmmo (tmp.GetComponent<Ammunition>());
		Destroy(ammo.gameObject);
	}
}
