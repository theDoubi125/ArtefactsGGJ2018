using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour
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
        if (Input.GetButtonDown(player.GetPlayerInputPrefix() + "_Action_1"))
        {
            Instantiate(projectilePrefab, transform);
        }
	}
}
