using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    Rigidbody body;
    Inventory inventory;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            inventory.TransferContentTo(player.GetComponentInChildren<Inventory>());
            Destroy(gameObject);
        }
    }
}
