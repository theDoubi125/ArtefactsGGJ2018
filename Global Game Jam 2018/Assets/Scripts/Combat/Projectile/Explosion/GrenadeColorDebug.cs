using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeColorDebug : MonoBehaviour
{
    MeshRenderer meshRenderer;
    TimedEffect timer;
    void Start ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        timer = GetComponent<TimedEffect>();
	}
	
	void Update ()
    {
        meshRenderer.material.color = new Color(1, timer.remainingTimeRatio, timer.remainingTimeRatio);
	}
}
