using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float projectileSpawnDistance = 1;
    public void AddBonus(Bonus bonus)
    {
        bonus.transform.SetParent(transform, false);
        bonus.OnBoundTo(transform);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == 0);
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == 0);
    }

    public void UseCurrentBonus(Vector3 position, Vector3 direction)
    {
        if(transform.childCount > 0)
        {
            Bonus bonus = transform.GetChild(0).GetComponent<Bonus>();
            Transform projectileTransform = Instantiate<Transform>(bonus.ProjectilePrefab);
            projectileTransform.position = position + direction.normalized * projectileSpawnDistance;
            projectileTransform.rotation = Quaternion.LookRotation(direction);
            projectileTransform.GetComponent<Inventory>().AddBonus(bonus);
        }
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
