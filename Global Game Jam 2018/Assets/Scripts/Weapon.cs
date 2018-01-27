using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform projectilePrefab;
    private PlayerController player;

	void Start ()
    {
        player = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown(player.GetPlayerInputPrefix() + "Action1"))
        {
            Transform projectile = Instantiate(projectilePrefab);
            projectile.position = transform.position;
        }
	}
}
