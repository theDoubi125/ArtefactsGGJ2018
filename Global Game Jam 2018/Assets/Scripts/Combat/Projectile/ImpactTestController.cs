using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactTestController : MonoBehaviour
{
	public float disappearSpeed = 1;
	float currentScale = 1;
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentScale -= disappearSpeed * Time.deltaTime;
		if (currentScale < 0)
			Destroy (gameObject);
		else transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
	}
}
