using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private TimedEffect timer;
    public Transform explosionPrefab;

	void OnEnable()
    {
        timer = GetComponent<TimedEffect>();
        if(timer != null)
            timer.OnTimeOver += Explode;
	}

    void OnDisable()
    {
        if(timer != null)
            timer.OnTimeOver -= Explode;
    }

	void Explode ()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
