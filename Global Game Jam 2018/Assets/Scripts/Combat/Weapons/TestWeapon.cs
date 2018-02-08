using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    private Weapon weapon;

    void Update()
    {

    }

    void OnEnable()
    {
        weapon = GetComponent<Weapon>();
        weapon.AttackPressed += AttackPressed;
        weapon.ThrowPressed += ThrowPressed;
    }

    void OnDisable()
    {
        weapon.AttackPressed -= AttackPressed;
        weapon.ThrowPressed -= ThrowPressed;
    }

    private void AttackPressed()
    {
        weapon.Attack();
    }

    private void ThrowPressed()
    {
        weapon.Throw();
    }
}
