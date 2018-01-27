using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
    public int playerIndex = 1;
    public Vector2 direction = Vector2.right;

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
        Vector2 direction = new Vector2(Input.GetAxis(playerInputPrefix + "Look_X"), Input.GetAxis(playerInputPrefix + "Look_Y"));
        float angle = Vector2.Angle(Vector2.right, direction);
        if (Vector3.Cross(new Vector3(direction.x, direction.y, 0), Vector3.right).z > 0)
            angle *= -1;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        if(direction.sqrMagnitude > 0)
            transform.SetPositionAndRotation(transform.position, rotation);
	}

    public string GetPlayerInputPrefix()
    {
        return playerInputPrefix;
    }
}
