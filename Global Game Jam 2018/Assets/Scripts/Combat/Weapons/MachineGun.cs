using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public WeaponAction actionToBind;
    public bool throwOnShoot = false;
	public int projectilePerShoot = 1;
    public float fireRate = 0.3f;
    private float fireTime = 0;
    public float precision = 10;
    private Weapon weapon;
    private Magazine magazine;
    private PlayerController player;
    private Inventory inventory;
    public float throwDistance = 2;

    public Transform projectilePrefab;

    bool isAttackPressed = false;

    void Update()
    {
        if(isAttackPressed)
        {
            fireTime += Time.deltaTime;
            while(fireTime > fireRate)
            {
                fireTime -= fireRate;
                Shoot();
            }
        }
    }

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

    private void AttackPressed()
    {
        isAttackPressed = true;
        Shoot();
        fireTime = 0;
    }

    private void AttackReleased()
    {
        isAttackPressed = false;
    }

    private void Shoot()
    {
        if (magazine.HasAmmo())
        {
			for (int i = 0; i < projectilePerShoot; i++)
			{
				Transform projectileTransform = Instantiate<Transform> (projectilePrefab);
				Vector3 angles = new Vector3 (0, Gaussian () * precision / 2f, 0);
				Vector3 shootDirection = Quaternion.Euler (angles) * player.GetWeaponDirection ().normalized;
				projectileTransform.position = transform.position + shootDirection * throwDistance;
				projectileTransform.rotation = Quaternion.LookRotation (shootDirection);
				Inventory projectileInventory = projectileTransform.GetComponentInChildren<Inventory> ();
				if (i == 0 && throwOnShoot && projectileInventory != null) {
					projectileInventory.AddWeapon (weapon);
					inventory.UpdateWeapons ();
				}
			}
            magazine.UseAmmo();
        }
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
