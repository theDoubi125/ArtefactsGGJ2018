using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float range = 5;
    public Vector2 force = new Vector2(10, 3);
    public float maxDamage = 50;

    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Hurtbox"));
        foreach(Collider collider in colliders)
        {
            Rigidbody body = collider.GetComponentInParent<Rigidbody>();
            Vector3 forceDirection = (collider.transform.position - transform.position);
            if (body != null)
            {
                forceDirection.y = 0;
                body.AddForce(force.x * forceDirection.normalized + Vector3.up * force.y, ForceMode.Impulse);
            }
            HealthController health = collider.GetComponentInParent<HealthController>();
            if(health != null)
            {
                health.Damage((int)(maxDamage * (1 - (forceDirection.magnitude / range))));
            }
        }
	}
}
