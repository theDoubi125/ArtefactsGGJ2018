using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollider : MonoBehaviour
{
    List<HealthController> entitiesAtRange = new List<HealthController>();

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        HealthController health = collider.GetComponentInParent<HealthController>();
        if (health != null)
        {
            entitiesAtRange.Add(health);
            Debug.Log(entitiesAtRange.Count);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        HealthController health = collider.GetComponentInParent<HealthController>();
        if (health != null)
        {
            entitiesAtRange.Remove(health);
            Debug.Log(entitiesAtRange.Count);
        }
    }

    public List<HealthController> GetEntitiesAtRange()
    {
        return entitiesAtRange;
    }
}
