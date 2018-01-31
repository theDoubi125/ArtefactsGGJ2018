using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
	public float defaultAcceleration = 10;
    public int playerIndex = 1;
    public float directionMinLength = 0.5f;
    public float maxSpeed = 5f;
    public float rotSpeed = 10;
    public float minSpeedForRot = 1;
    public float groundDetectionRange = 1.2f;

    private Vector3 weaponDirection = Vector2.right;

    public float cursorDistance = 3;

    private string playerInputPrefix;
    private Rigidbody body;
    private CharacterAnimation animationController;

    private Quaternion currentRotation = Quaternion.identity;
    private Quaternion targetRotation;

    private Vector3 currentTargetDirection = Vector3.right;
    private Vector3 currentMovementDirection;

    public Quaternion initialRotation;

    private Transform cursorTransform;
    private CharacterMeshComponent characterMesh;

	public GameObject personnalHUD;

    public bool isOnGround = false;

	public Sprite characterSprite;
	public Color playerColor;

    private MeleeColliderRotator colliderRotator;

	void Awake ()
    {
        if (!GameController.instance.IsPlayerControlled(playerIndex))
            gameObject.SetActive(false);
        else 
            playerInputPrefix = GameController.instance.GetPlayerInputPrefix(playerIndex);

        body = GetComponent<Rigidbody>();
        animationController = GetComponent<CharacterAnimation>();

        CursorController cursorController = GetComponentInChildren<CursorController>();
        if(cursorController != null)
            cursorTransform = cursorController.transform;
        characterMesh = transform.GetComponentInChildren<CharacterMeshComponent>();
        colliderRotator = GetComponentInChildren<MeleeColliderRotator>();
	}

	void Start ()
	{
		personnalHUD.transform.GetChild(0).GetComponent<Image>().sprite = characterSprite;
	}

    void FixedUpdate()
    {
        if (!GameController.instance.isGamePaused)
        {
            Vector3 movementDirection = new Vector3(Input.GetAxis(playerInputPrefix + "Move_X"), 0, Input.GetAxis(playerInputPrefix + "Move_Y"));
            if (isOnGround)
                body.AddForce(acceleration * movementDirection.normalized);
            else
                body.AddForce(acceleration / 5 * movementDirection.normalized);
            if (movementDirection.magnitude > directionMinLength)
                currentMovementDirection = movementDirection;
        }
    }
	
	void Update ()
    {
        if (!GameController.instance.isGamePaused)
        {
            isOnGround = Physics.Raycast(transform.position, Vector3.down, groundDetectionRange);
            Vector3 targetDirection = new Vector3(Input.GetAxis(playerInputPrefix + "Look_X"), 0, -Input.GetAxis(playerInputPrefix + "Look_Y"));
            if (targetDirection.sqrMagnitude > directionMinLength * directionMinLength)
            {
                currentTargetDirection = targetDirection;
                weaponDirection = currentTargetDirection.normalized;
            }
            if (cursorTransform != null)
                cursorTransform.rotation = Quaternion.LookRotation(weaponDirection);
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
            if (characterMesh != null)
                characterMesh.SetRotation(currentRotation.eulerAngles.y);
            if (colliderRotator != null)
                colliderRotator.SetRotation(currentRotation.eulerAngles.y);

            animationController.SetSpeedRatio(body.velocity.magnitude / maxSpeed);
        }
	}

    public Vector3 GetWeaponDirection()
    {
        return currentTargetDirection;
    }

    public Vector3 GetMovementDirection()
    {
        return currentMovementDirection;
    }

    public string GetPlayerInputPrefix()
    {
        return playerInputPrefix;
    }
}
