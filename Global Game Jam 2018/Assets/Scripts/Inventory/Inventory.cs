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
			Weapon bonus = child.GetComponent<Weapon>();
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

	public void ThrowCurrentWeapon(Vector3 position, Vector3 direction)
    {
        if(transform.childCount > 0)
        {
			Weapon bonus = transform.GetChild(0).GetComponent<Weapon>();
			Transform projectileTransform = Instantiate<Transform>(bonus.throwProjectilePrefab);
            projectileTransform.position = position + direction.normalized * projectileSpawnDistance;
            projectileTransform.rotation = Quaternion.LookRotation(direction);
			projectileTransform.GetComponentInChildren<Inventory>().AddWeapon(bonus);
        }
        if (transform.childCount > 0 && mustEnableFirstItem)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

	public void Attack(Vector3 position, Vector3 direction)
	{
		if(transform.childCount > 0)
		{
			Weapon bonus = transform.GetChild(0).GetComponent<Weapon>();
			
		}
	}
}
