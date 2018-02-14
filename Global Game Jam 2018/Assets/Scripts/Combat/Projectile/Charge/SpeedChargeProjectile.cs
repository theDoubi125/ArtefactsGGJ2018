using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChargeProjectile : MonoBehaviour
{
	public float maxSpeed = 10;

	void Start ()
	{
		GetComponent<Rigidbody> ().velocity = transform.forward * GetComponent<ChargeProjectile> ().charge * maxSpeed;
	}
}
