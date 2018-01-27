using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
    public int playerIndex = 1;
    public float directionMinLength = 0.5f;
    public float maxSpeed = 5f;
    public float rotSpeed = 10;
    public float minSpeedForRot = 1;
    public Transform cursor;

    private Vector3 weaponDirection = Vector2.right;

    float cursorDistance = 10;

    private string playerInputPrefix;
    private Rigidbody body;
    private CharacterAnimation animationController;

    private Quaternion currentRotation = Quaternion.identity;
    private Quaternion targetRotation;

	void Start ()
    {
        playerInputPrefix = "Player" + playerIndex + "_";
        body = GetComponent<Rigidbody>();
        animationController = GetComponent<CharacterAnimation>();
	}
	
	void Update ()
    {
        body.AddForce(acceleration * (new Vector3(Input.GetAxis(playerInputPrefix + "Move_X"), 0, Input.GetAxis(playerInputPrefix + "Move_Y"))).normalized);
        Vector3 inputWeaponDirection = new Vector3(Input.GetAxis(playerInputPrefix + "Look_X"), 0, Input.GetAxis(playerInputPrefix + "Look_Y"));
        if (inputWeaponDirection.sqrMagnitude > directionMinLength * directionMinLength)
            weaponDirection = inputWeaponDirection.normalized;
        cursor.transform.position = transform.position + weaponDirection * cursorDistance;
        Vector3 direction = body.velocity.normalized;
        if (body.velocity.sqrMagnitude > minSpeedForRot * minSpeedForRot)
        {
            float angle = Vector3.Angle(Vector2.right, direction);
            if (direction.z > 0)
                angle *= -1;
            targetRotation = Quaternion.AngleAxis(angle, Vector3.up);
        }

        float deltaAngle = Quaternion.Angle(currentRotation, targetRotation);
        Debug.Log(deltaAngle);
        currentRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotSpeed * Time.deltaTime); 
        transform.SetPositionAndRotation(transform.position, currentRotation);

        animationController.SetSpeedRatio(body.velocity.magnitude / maxSpeed);
	}

    public string GetPlayerInputPrefix()
    {
        return playerInputPrefix;
    }
}
