using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public float fireRate = 0.3f;
    private float fireTime = 0;
    public float precision = 10;
    private Weapon weapon;
    private Magazine magazine;
    private PlayerController player;

    public Vector3 angles;

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
        isAttackPressed = true;
        Shoot();
        fireTime = 0;
    }

    private void AttackReleased()
    {
        isAttackPressed = false;
    }

    private void ThrowPressed()
    {
        weapon.Throw();
    }

    private void Shoot()
    {
        if (magazine.HasAmmo())
        {
            Transform projectileTransform = Instantiate<Transform>(projectilePrefab);
            Vector3 angles = new Vector3(0, Random.Range(-precision/2, precision/2), 0);
            Vector3 shootDirection = Quaternion.Euler(angles) * player.GetWeaponDirection().normalized;
            Debug.Log(shootDirection + " " + angles);
            projectileTransform.position = transform.position + shootDirection * weapon.throwDistance;
            projectileTransform.rotation = Quaternion.LookRotation(shootDirection);
            magazine.UseAmmo();
        }
    }
}
