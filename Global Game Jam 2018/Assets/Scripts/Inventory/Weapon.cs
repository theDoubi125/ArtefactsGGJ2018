using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform throwProjectilePrefab;
	public Transform attackProjectilePrefab;
	public int clipAmmo = 5;
	private int ammoLeft = 0;

	void Start()
	{
		ammoLeft = clipAmmo;
	}

    public virtual void OnBoundTo(Transform transform)
    {
		ammoLeft = clipAmmo;
    }
}
