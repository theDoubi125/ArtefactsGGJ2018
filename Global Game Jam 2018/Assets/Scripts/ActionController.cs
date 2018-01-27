using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour 
{
    private PlayerController player;
    private Rigidbody body;
    public float dashForce = 10;
    public Vector2 jumpForce = new Vector2(3, 20);
    public float groundDetectionRange = 1.2f;
    public float dashReloadDuration = 2;
    private float dashReloadTime = 0;

    void Start ()
    {
        player = GetComponent<PlayerController>();
        body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        string dashInputName = player.GetPlayerInputPrefix() + "Dash";
        if (dashReloadTime <= 0 && Input.GetButtonDown(dashInputName))
        {
            body.AddForce(transform.right * dashForce, ForceMode.VelocityChange);
            dashReloadTime = dashReloadDuration;
            GetComponent<CharacterAnimation>().Dash();
        }
        if (dashReloadTime > 0)
            dashReloadTime -= Time.deltaTime;
        if (Physics.Raycast(transform.position, Vector3.down, groundDetectionRange) && Input.GetButtonDown(player.GetPlayerInputPrefix() + "Jump"))
        {
            body.AddForce(transform.up * jumpForce.y + transform.right * jumpForce.x, ForceMode.VelocityChange);
            GetComponent<CharacterAnimation>().Jump();
        }
	}
}
