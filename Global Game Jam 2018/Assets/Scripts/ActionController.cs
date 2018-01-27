using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour 
{
    private PlayerController player;
    private Rigidbody body;
    public float dashForce = 10;
    public Vector2 jumpForce = new Vector2(3, 20);
    public float jumpReloadDuration = 2;

    void Start ()
    {
        player = GetComponent<PlayerController>();
        body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (Input.GetButtonDown(player.GetPlayerInputPrefix() + "Dash"))
        {
            body.AddForce(transform.right * dashForce, ForceMode.VelocityChange);
        }
        string buttonName = player.GetPlayerInputPrefix() + "Jump";
        if (Input.GetButtonDown(buttonName))
        {
            body.AddForce(transform.up * jumpForce.y + transform.right * jumpForce.x, ForceMode.VelocityChange);
        }
	}
}
