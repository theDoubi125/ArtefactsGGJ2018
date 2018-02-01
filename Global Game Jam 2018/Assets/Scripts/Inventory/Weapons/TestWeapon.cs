using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
    public float bumpFrequency = 1;
    public float bumpStrength = 5;
    float bumpTime = 0;

    PlayerController playerController;
    Rigidbody body;

	void Start ()
    {
        OnBoundTo(transform);
	}
	
	void Update ()
    {
        bumpTime += Time.deltaTime;
        if(bumpTime > bumpFrequency)
        {
            if (playerController != null)
                body.AddForce(Vector3.up * bumpStrength, ForceMode.Impulse);
            bumpTime -= bumpFrequency;
        }
	}

    public override void OnBoundTo(Transform transform)
    {
        base.OnBoundTo(transform);
        playerController = transform.GetComponentInParent<PlayerController>();
        body = transform.GetComponentInParent<Rigidbody>();
    }
}
