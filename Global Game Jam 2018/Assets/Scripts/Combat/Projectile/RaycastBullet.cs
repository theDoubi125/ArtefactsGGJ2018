using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBullet : MonoBehaviour
{
    public float range = 100;
	public float knockBackForce = 10;
    public Transform impactPrefab;
    public Transform trailPrefab;
	public int damage = 10;

	void Start ()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, range))
        {
            Transform trail = Instantiate<Transform>(trailPrefab, raycastHit.point, Quaternion.identity);
            trail.GetComponent<BulletTrail>().startPoint = transform.position;
			if (raycastHit.rigidbody != null && raycastHit.rigidbody.GetComponent<PlayerController> () != null)
			{
				raycastHit.rigidbody.AddForce (transform.forward * knockBackForce, ForceMode.Impulse);
				if (raycastHit.rigidbody.GetComponent<HealthController> () != null)
					raycastHit.rigidbody.GetComponent<HealthController> ().Damage (damage);
			}
			
        }

        Destroy(gameObject);
	}
}
