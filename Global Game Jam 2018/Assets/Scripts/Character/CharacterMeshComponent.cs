using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshComponent : MonoBehaviour {

    Vector3 initialRotation;
    public float angle;
	void Awake ()
    {
        initialRotation = transform.localEulerAngles;;
	}
	
    public void SetRotation(float angle)
    {
        transform.localEulerAngles = initialRotation + new Vector3(0, 0, angle);
    }
}
