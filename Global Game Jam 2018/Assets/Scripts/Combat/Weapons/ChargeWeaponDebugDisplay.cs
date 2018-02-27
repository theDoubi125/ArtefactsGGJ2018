using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeaponDebugDisplay : MonoBehaviour
{
    public float scaleRatio = 1;
    private ChargeWeapon weapon;

    void OnEnable()
    {
        weapon = transform.parent.GetComponentInChildren<ChargeWeapon>();
    }

    void Update()
    {
        if (weapon != null)
        {
            transform.localScale = new Vector3(weapon.chargeRatio, weapon.chargeRatio, weapon.chargeRatio) * scaleRatio;
        }
        else transform.localScale = Vector3.zero;
    }
}
