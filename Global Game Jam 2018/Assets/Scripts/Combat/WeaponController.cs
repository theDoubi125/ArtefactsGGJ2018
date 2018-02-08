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

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<PlayerController>();
		magazine.Capacity = maxCapacity;
	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void StealWeapon(WeaponController victim)
	{
		if (victim.magazine.Count > 0) {
			Transform projectile = Instantiate (victim.bullet);
			projectile.position = victim.transform.position + Vector3.up;
			projectile.rotation = Quaternion.LookRotation (player.GetWeaponDirection ()); 	
		}
	}
}
