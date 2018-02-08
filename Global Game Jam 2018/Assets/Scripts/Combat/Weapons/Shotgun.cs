using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
	public float fireRate = 1.5f;
	private float fireTime = 1.5f;
	public float precision = 20;
	private Weapon weapon;
	private Magazine magazine;
	private PlayerController player;
	public int bulletCount = 5;

	public Vector3 angles;

	public Transform projectilePrefab;

	void Update()
	{
		fireTime += Time.deltaTime;
	}

	void OnEnable()
	{
		player = GetComponentInParent<PlayerController>();
		weapon = GetComponent<Weapon>();
		weapon.AttackPressed += AttackPressed;
		weapon.AttackReleased += AttackReleased;
		weapon.ThrowPressed += ThrowPressed;
		magazine = GetComponent<Magazine>();
	}

	void OnDisable()
	{
		weapon.AttackPressed -= AttackPressed;
		weapon.ThrowPressed -= ThrowPressed;
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

	private void ThrowPressed()
	{
		weapon.Throw();
	}

	private void Shoot()
	{
		Transform projectileTransform = Instantiate<Transform>(projectilePrefab);
        float angle = Gaussian() * precision / 2f;
		Vector3 angles = new Vector3(0, angle, 0);
		Vector3 shootDirection = Quaternion.Euler(angles) * player.GetWeaponDirection().normalized;
		projectileTransform.position = transform.position + shootDirection * weapon.throwDistance;
		projectileTransform.rotation = Quaternion.LookRotation(shootDirection);
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
