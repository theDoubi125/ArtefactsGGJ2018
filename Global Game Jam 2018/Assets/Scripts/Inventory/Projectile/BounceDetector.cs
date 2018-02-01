using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceDetector : MonoBehaviour
{
    public delegate void BounceDelegate();
    public BounceDelegate onBounce;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
            onBounce();
    }
}
