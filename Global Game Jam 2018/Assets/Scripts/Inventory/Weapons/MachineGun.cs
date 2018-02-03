using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public float fireRate = 0.3f;
    private float fireTime = 0;
    private Weapon weapon;

    bool isAttackPressed = false;

    void Update()
    {
        if(isAttackPressed)
        {
            fireTime += Time.deltaTime;
            while(fireTime > fireRate)
            {
                fireTime -= fireRate;
                weapon.Attack();
            }
        }
    }

    void OnEnable()
    {
        weapon = GetComponent<Weapon>();
        weapon.AttackPressed += AttackPressed;
        weapon.AttackReleased += AttackReleased;
        weapon.ThrowPressed += ThrowPressed;
    }

    void OnDisable()
    {
        weapon.AttackPressed -= AttackPressed;
        weapon.ThrowPressed -= ThrowPressed;
    }

    private void AttackPressed()
    {
        isAttackPressed = true;
        weapon.Attack();
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
}
