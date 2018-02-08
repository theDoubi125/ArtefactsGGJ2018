using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollider : MonoBehaviour
{
    List<HealthController> entitiesAtRange = new List<HealthController>();
    public bool isTransmitterInRange = false;

    void OnTriggerEnter(Collider collider)
    {
        HealthController health = collider.GetComponentInParent<HealthController>();
        if (health != null)
        {
            entitiesAtRange.Add(health);
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
