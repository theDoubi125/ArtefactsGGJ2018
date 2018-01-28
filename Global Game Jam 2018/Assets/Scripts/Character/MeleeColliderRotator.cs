using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeColliderRotator : MonoBehaviour
{
    public void SetRotation(float angle)
    {
        transform.localEulerAngles = new Vector3(0, angle, 0);
    }
}
