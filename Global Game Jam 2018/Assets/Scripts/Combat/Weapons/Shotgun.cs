using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public WeaponAction actionToBind;
    public bool throwOnShoot = false;
    public float fireRate = 1.5f;
	private float fireTime = 1.5f;
	public float precision = 20;
	private Weapon weapon;
	private Magazine magazine;
	private PlayerController player;
    private Inventory inventory;
	public int bulletCount = 5;

    public float throwDistance = 2;

	public Transform projectilePrefab;

	void Update()
	{
		fireTime += Time.deltaTime;
	}

	void OnEnable()
	{
		player = GetComponentInParent<PlayerController>();
		weapon = GetComponent<Weapon>();
        weapon.BindToAction(actionToBind, AttackPressed, AttackReleased);
		magazine = GetComponent<Magazine>();
        inventory = GetComponentInParent<Inventory>();
    }

	void OnDisable()
	{
		weapon.AttackPressed -= AttackPressed;
	}

	private void AttackPressed()
	{
		if (magazine.HasAmmo() && fireTime > fireRate)
		{
			for(int i=0; i<bulletCount; i++)
				Shoot ();
			fireTime = 0;
			magazine.UseAmmo();
		}
	}

	private void AttackReleased()
	{
		
	}

	private void Shoot()
	{
		Transform projectileTransform = Instantiate<Transform>(projectilePrefab);
        float angle = Gaussian() * precision / 2f;
		Vector3 angles = new Vector3(0, angle, 0);
		Vector3 shootDirection = Quaternion.Euler(angles) * player.GetWeaponDirection().normalized;
		projectileTransform.position = transform.position + shootDirection * throwDistance;
		projectileTransform.rotation = Quaternion.LookRotation(shootDirection);
        Inventory projectileInventory = projectileTransform.GetComponentInChildren<Inventory>();
        if (throwOnShoot && projectileInventory != null)
        {
            projectileInventory.AddWeapon(weapon);
            inventory.UpdateWeapons();
        }
        magazine.UseAmmo();

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
