using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public Vector2 initialSpeed = new Vector2(10, 5);
    private Inventory inventory;
    private Rigidbody body;

    void Start ()
    {
        body = GetComponentInParent<Rigidbody>();
        body.AddForce(transform.forward * initialSpeed.x + transform.up * initialSpeed.y, ForceMode.VelocityChange);
        inventory = transform.parent.GetComponentInChildren<Inventory>();
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
}
