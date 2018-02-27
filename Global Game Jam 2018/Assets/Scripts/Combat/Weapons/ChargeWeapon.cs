using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MonoBehaviour
{
	public WeaponAction actionToBind;
	public bool throwOnShoot = false;

	public Transform projectilePrefab;
	public float precision = 10;
	private Weapon weapon;
	private Magazine magazine;
	private PlayerController player;
	private Inventory inventory;
	public float throwDistance = 2;
	public float projectilePerShot = 1;

	public float chargeDuration = 3;
	private bool isCharging = false;
	private float charge = 0;

	public float throwAngle = 10;

    public float chargeRatio { get { if(isCharging) return charge; return 0; } }

	void OnEnable()
	{
		player = GetComponentInParent<PlayerController>();
		inventory = GetComponentInParent<Inventory>();
		weapon = GetComponent<Weapon>();
		weapon.BindToAction(actionToBind, AttackPressed, AttackReleased);
		weapon.AttackReleased += AttackReleased;
		magazine = GetComponent<Magazine>();
	}

	void OnDisable()
	{
		weapon.UnbindAction(actionToBind, AttackPressed, AttackReleased);
	}

	void AttackPressed()
	{
		isCharging = true;
		charge = 0;
	}

	void AttackReleased()
	{
		if (isCharging)
		{
			List<Transform> projectiles = Shoot();
			foreach(Transform projectile in projectiles)
			{
				ChargeProjectile chargeProjectile = projectile.GetComponent<ChargeProjectile> ();
                if(chargeProjectile != null)
    				chargeProjectile.charge = charge;
			}
			isCharging = false;
		}
	}

	void Update()
	{
		if (isCharging)
		{
			charge += Time.deltaTime / chargeDuration;
			if (charge > 1)
			{
				charge = 1;
			}
		}
	}

	private List<Transform> Shoot()
	{
		List<Transform> result = new List<Transform>();
		if (magazine.HasAmmo())
		{
			for (int i = 0; i < projectilePerShot; i++)
			{
				Transform projectileTransform = Instantiate<Transform> (projectilePrefab);
				result.Add (projectileTransform);
				Vector3 angles = new Vector3 (0, Gaussian () * precision / 2f, throwAngle);
				Vector3 shootDirection = Quaternion.Euler (angles) * player.GetWeaponDirection ().normalized;
				projectileTransform.position = transform.position + shootDirection * throwDistance;
				projectileTransform.rotation = Quaternion.LookRotation (shootDirection);
				Inventory projectileInventory = projectileTransform.GetComponentInChildren<Inventory> ();
				if (i == 0 && throwOnShoot && projectileInventory != null) 
				{
					projectileInventory.AddWeapon (weapon);
					inventory.UpdateWeapons ();
				}
			}
			magazine.UseAmmo();
		}
		return result;
	}

	public static float Gaussian()
	{
		float u, v, S;

		do
		{
			u = 2.0f * Random.value - 1.0f;
			v = 2.0f * Random.value - 1.0f;
			S = u * u + v * v;
		}
		while (S >= 1.0f);

		float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
		return u * fac;
	}
}
