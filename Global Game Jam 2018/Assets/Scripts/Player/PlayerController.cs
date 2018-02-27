using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType
{
    Attack,
    Throw
}

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

    public delegate void InputDelegate(InputType type);
    public InputDelegate OnInputPressed;
    public InputDelegate OnInputReleased;

   private bool isThrowPressed = false;
    private bool isAttackPressed = false;
    public float InputThreshold = 0.5f;

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
				float angle = Vector3.Angle(Vector2.right, GetWeaponDirection());
                if (direction.z > 0)
                    angle *= -1;
                targetRotation = Quaternion.AngleAxis(angle, Vector3.up);
            }
            
            //urrentRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotSpeed * Time.deltaTime); 
            if (characterMesh != null)
				characterMesh.transform.rotation = Quaternion.LookRotation(weaponDirection);
            if (colliderRotator != null)
				colliderRotator.transform.rotation = Quaternion.LookRotation(weaponDirection);

            animationController.SetSpeedRatio(body.velocity.magnitude / maxSpeed);
        }

        if(Input.GetAxis(GetPlayerInputPrefix() + "Action1")  != 0)
            Debug.Log(Input.GetAxis(GetPlayerInputPrefix() + "Action1"));
        
        if (Input.GetAxis(GetPlayerInputPrefix() + "Action1") < InputThreshold && isThrowPressed)
        {
            isThrowPressed = false;
            if (OnInputReleased != null)
                OnInputReleased(InputType.Throw);
        }
        if (Input.GetAxis(GetPlayerInputPrefix() + "Action1") > InputThreshold && !isThrowPressed)
        {
            isThrowPressed = true;
            if (OnInputPressed != null)
                OnInputPressed(InputType.Throw);
        }
        else
        {
            if (Input.GetAxis(GetPlayerInputPrefix() + "Action2") < InputThreshold && isAttackPressed)
            {
                isAttackPressed = false;
                if (OnInputReleased != null)
                    OnInputReleased(InputType.Attack);
            }
            if (Input.GetAxis(GetPlayerInputPrefix() + "Action2") > InputThreshold && !isAttackPressed)
            {
                isAttackPressed = true;
                if (OnInputPressed != null)
                    OnInputPressed(InputType.Attack);
            }
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
