using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int maxAmmo = 10;
    private int currentAmmo = 0;

    void OnEnable()
    {
        currentAmmo = maxAmmo; 
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    public void UseAmmo()
    {
        currentAmmo--;
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
