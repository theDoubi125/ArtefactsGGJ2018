using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatefond: MonoBehaviour {
	public float rotSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.localPosition, Vector3.up, rotSpeed * Time.deltaTime);
	}
}
