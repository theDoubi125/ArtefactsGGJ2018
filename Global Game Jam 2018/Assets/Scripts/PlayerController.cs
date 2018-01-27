using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
    public int playerIndex = 1;

    private string playerInputPrefix;
    private Rigidbody2D body;

	void Start ()
    {
        playerInputPrefix = "Player" + playerIndex + "_";
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        body.AddForce(acceleration * new Vector2(Input.GetAxis(playerInputPrefix + "Move_X"), Input.GetAxis(playerInputPrefix + "Move_Y")));
	}

    public string GetPlayerInputPrefix()
    {
        return playerInputPrefix;
    }
}
