using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollider : MonoBehaviour
{
    List<HealthController> entitiesAtRange = new List<HealthController>();
    public bool isTransmitterInRange = false;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        HealthController health = collider.GetComponentInParent<HealthController>();
        if (health != null)
        {
            entitiesAtRange.Add(health);
            Debug.Log(entitiesAtRange.Count);
        }
        else if (collider.GetComponent<Transmitter>() != null)
        {
            isTransmitterInRange = true;
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
        else if (collider.GetComponent<Transmitter>() != null)
        {
            isTransmitterInRange = false;
        }
    }

    public List<HealthController> GetEntitiesAtRange()
    {
        return entitiesAtRange;
    }
}
