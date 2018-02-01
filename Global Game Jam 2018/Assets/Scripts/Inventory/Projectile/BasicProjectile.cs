using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public Vector2 initialSpeed = new Vector2(10, 5);
    public Transform cratePrefab;
    public int maxBounce = 3;

    private int bounceCount = 0;
    private Inventory inventory;
    private Rigidbody body;
    private BounceDetector bounceDetector;

    void Start ()
    {
        body = GetComponentInParent<Rigidbody>();
        body.AddForce(transform.forward * initialSpeed.x + transform.up * initialSpeed.y, ForceMode.VelocityChange);
        inventory = transform.parent.GetComponentInChildren<Inventory>();
        bounceDetector = body.GetComponent<BounceDetector>();
        if (bounceDetector != null)
            bounceDetector.onBounce += OnBounce;
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            inventory.TransferContentTo(player.GetComponentInChildren<Inventory>());
            Destroy(body.gameObject);
        }
    }

    void OnBounce()
    {
        if (bounceCount >= maxBounce)
        {
            Transform crateInstance = Instantiate<Transform>(cratePrefab, transform.position, transform.rotation);
            inventory.TransferContentTo(crateInstance.GetComponentInChildren<Inventory>());
            Destroy(transform.parent.gameObject);
        }
        bounceCount++;
    }
}
