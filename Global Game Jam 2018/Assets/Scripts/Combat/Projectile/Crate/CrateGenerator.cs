using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateGenerator : MonoBehaviour
{
    public Transform cratePrefab;
    public int maxBounce = 3;

    private int bounceCount = 0;
    private Inventory inventory;
    private Rigidbody body;
    private BounceDetector bounceDetector;

    void Start()
    {
        body = GetComponentInParent<Rigidbody>();
        inventory = transform.parent.GetComponentInChildren<Inventory>();
        bounceDetector = body.GetComponent<BounceDetector>();
        if (bounceDetector != null)
            bounceDetector.onBounce += OnBounce;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        /*PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            inventory.TransferContentTo(player.GetComponentInChildren<Inventory>());
            Destroy(body.gameObject);
        }*/
    }

    void OnBounce()
    {
        if (bounceCount >= maxBounce)
        {
			Transform crateInstance = Instantiate<Transform>(cratePrefab, transform.position, Quaternion.identity);
            inventory.TransferContentTo(crateInstance.GetComponentInChildren<Inventory>());
            Destroy(transform.parent.gameObject);
        }
        bounceCount++;
    }
}
