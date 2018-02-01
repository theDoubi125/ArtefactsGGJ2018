using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float projectileSpawnDistance = 1;

    public void AddWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(transform, false);
        weapon.OnBoundTo(transform);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == 0);
    }

    public void TransferContentTo(Inventory target)
    {
        while(transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
			Weapon bonus = child.GetComponent<Weapon>();
            bonus.OnBoundTo(target.transform);
            if (target.transform.childCount > 0)
                child.gameObject.SetActive(false);
            child.SetParent(target.transform);
        }
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == 0);
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
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

	public void Attack(Vector3 position, Vector3 direction)
	{
		if(transform.childCount > 0)
		{
			Weapon bonus = transform.GetChild(0).GetComponent<Weapon>();
			Transform projectileTransform = Instantiate<Transform>(bonus.attackProjectilePrefab);
			projectileTransform.position = position + direction.normalized * projectileSpawnDistance;
			projectileTransform.rotation = Quaternion.LookRotation(direction);
		}
	}
}
