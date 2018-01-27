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

    public float cursorDistance = 3;

    private string playerInputPrefix;
    private Rigidbody body;
    private CharacterAnimation animationController;

    private Quaternion currentRotation = Quaternion.identity;
    private Quaternion targetRotation;

    private Vector3 currentTargetDirection;

	void Start ()
    {
        if (!GameController.instance.IsPlayerControlled(playerIndex))
            gameObject.SetActive(false);
        playerInputPrefix = GameController.instance.GetPlayerInputPrefix(playerIndex);
        body = GetComponent<Rigidbody>();
        animationController = GetComponent<CharacterAnimation>();
	}
	
	void Update ()
    {
        Debug.Log(playerInputPrefix);
        body.AddForce(acceleration * (new Vector3(Input.GetAxis(playerInputPrefix + "Move_X"), 0, Input.GetAxis(playerInputPrefix + "Move_Y"))).normalized);
        Vector3 targetDirection = new Vector3(Input.GetAxis(playerInputPrefix + "Look_X"), 0, -Input.GetAxis(playerInputPrefix + "Look_Y"));
        if (targetDirection.sqrMagnitude > directionMinLength * directionMinLength)
        {
            currentTargetDirection = targetDirection;
            weaponDirection = currentTargetDirection.normalized;
        }
        if(cursor != null)
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
        currentRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotSpeed * Time.deltaTime); 
        transform.SetPositionAndRotation(transform.position, currentRotation);

        animationController.SetSpeedRatio(body.velocity.magnitude / maxSpeed);
	}

    public Vector3 GetWeaponDirection()
    {
        return currentTargetDirection;
    }

    public string GetPlayerInputPrefix()
    {
        return playerInputPrefix;
    }
}
