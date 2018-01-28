using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshComponent : MonoBehaviour {

    Quaternion initialRotation;
    public float angle;
	void Awake ()
    {
        initialRotation = transform.localRotation;
	}
	
    public void SetRotation(float angle)
    {
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
