using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollider : MonoBehaviour
{
    List<HealthController> entitiesAtRange = new List<HealthController>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        HealthController health = collider.GetComponent<HealthController>();
        if (health != null)
        {
            entitiesAtRange.Add(health);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        HealthController health = collider.GetComponent<HealthController>();
        if (health != null)
        {
            entitiesAtRange.Remove(health);
        }
    }

    public List<HealthController> GetEntitiesAtRange()
    {
        return entitiesAtRange;
    }
}
