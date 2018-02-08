using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshComponent : MonoBehaviour {

    public Transform MeshPrefab;
    Vector3 initialRotation;
    public float angle;

	void Awake ()
    {
        initialRotation = transform.localEulerAngles;
        Instantiate<Transform>(MeshPrefab, transform);
    }
	
    public void SetRotation(float angle)
    {
        transform.localEulerAngles = initialRotation + new Vector3(0, 0, angle);
    }
}
