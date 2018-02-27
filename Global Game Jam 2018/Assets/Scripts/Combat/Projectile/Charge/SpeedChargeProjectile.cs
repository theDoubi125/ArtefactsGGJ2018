using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChargeProjectile : MonoBehaviour
{
    public float minSpeed = 10;
	public float maxSpeed = 10;

	void Start ()
	{
		GetComponent<Rigidbody> ().velocity = transform.forward * (minSpeed + GetComponent<ChargeProjectile> ().charge * (maxSpeed - minSpeed));
	}
}
