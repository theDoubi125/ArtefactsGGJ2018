using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBullet : MonoBehaviour
{
    public float range = 100;
    public Transform impactPrefab;
    public Transform trailPrefab;

	void Start ()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, range))
        {
            Transform impact = Instantiate<Transform>(impactPrefab, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
            Transform trail = Instantiate<Transform>(trailPrefab, raycastHit.point, Quaternion.identity);
            trail.GetComponent<BulletTrail>().startPoint = transform.position;
        }

        Destroy(gameObject);
	}
}
