using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public Transform projectilePrefab;
	public List<GameObject> magazine;
	public int maxCapacity = 5;
	public PlayerController player;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<PlayerController>();
		magazine.Capacity = maxCapacity;
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

	public void CreateProjectile()
	{
		Transform projectile = Instantiate(projectilePrefab);
        projectile.position = transform.position + player.GetWeaponDirection() * 0.5f;
        projectile.rotation = Quaternion.LookRotation(player.GetWeaponDirection()); 	
		projectile.gameObject.AddComponent <Ammunition>();
		Ammunition tmp = projectile.gameObject.GetComponent<Ammunition>();
		tmp.behaviorchoice = magazine[0].GetComponent<Ammunition>().behaviorchoice;
		tmp.bonuschoice = magazine[0].GetComponent<Ammunition>().bonuschoice;
		tmp.shooter = player;
		magazine [0].GetComponent<AmmunitionBonus> ().StopAction (player);
		Destroy(magazine[0]);
		magazine.RemoveAt(0);
	}

	public void HarvestCrate (Ammunition ammo)
	{
		Debug.Log("Crate harvested");
		GameObject tmp = new GameObject();
		tmp.AddComponent<Ammunition>();
		tmp.GetComponent<Ammunition>().behaviorchoice = ammo.behaviorchoice;
		tmp.GetComponent<Ammunition>().bonuschoice = ammo.bonuschoice;
		tmp.transform.SetParent (transform);
		magazine.Add(tmp);
		Destroy(ammo.gameObject);
	}
}
