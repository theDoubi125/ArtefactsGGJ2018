using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float projectileSpawnDistance = 1;
    public bool mustEnableFirstItem = false;

    public void AddWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(transform, false);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(mustEnableFirstItem && i == 0);
        }
    }

    public void UpdateWeapons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(mustEnableFirstItem && i == 0);
        }
    }

    public void TransferContentTo(Inventory target)
    {
        while(transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            bool mustActivate = target.mustEnableFirstItem && target.transform.childCount == 0;
            child.SetParent(target.transform);
            child.gameObject.SetActive(mustActivate);
        }
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(mustEnableFirstItem && i == 0);
    }
}
