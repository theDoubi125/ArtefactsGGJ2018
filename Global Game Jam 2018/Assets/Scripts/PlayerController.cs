using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
    public int playerIndex = 1;
    public Vector2 direction = Vector2.right;

    private string playerInputPrefix;
    private Rigidbody body;

	void Start ()
    {
        playerInputPrefix = "Player" + playerIndex + "_";
        body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        body.AddForce(acceleration * new Vector3(Input.GetAxis(playerInputPrefix + "Move_X"), 0, Input.GetAxis(playerInputPrefix + "Move_Y")));
        Vector3 direction = new Vector3(Input.GetAxis(playerInputPrefix + "Look_X"), 0, Input.GetAxis(playerInputPrefix + "Look_Y"));
        float angle = Vector3.Angle(Vector2.right, direction);
        if (direction.z < 0)
            angle *= -1;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        if(direction.sqrMagnitude > 0)
            transform.SetPositionAndRotation(transform.position, rotation);
	}

    public string GetPlayerInputPrefix()
    {
        return playerInputPrefix;
    }
}
