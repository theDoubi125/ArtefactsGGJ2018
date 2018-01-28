using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour 
{
    private PlayerController player;
    private Rigidbody body;
    public float dashForce = 10;
    public Vector2 jumpForce = new Vector2(3, 20);
    public float dashReloadDuration = 2;
	public float dashReloadBonusFactor = 0;
	public bool isInRangeOfTransmitter = false;
    private float dashReloadTime = 0;
    private float meleeReloadTime = 0;
    public float meleeReloadDuration = 1;
    private MeleeAttackCollider meleeHitbox;
    public Vector2 meleePushback = new Vector2(10, 20);
	public int meleeDamage = 5;
    CharacterAnimation characterAnimation;
    WeaponController weaponController;
    Transmitter transmitter;
    bool isDashPressed = false;
    float InputThreshold = 0.5f;

    void Start ()
    {
        player = GetComponent<PlayerController>();
        body = GetComponent<Rigidbody>();
        meleeHitbox = GetComponentInChildren<MeleeAttackCollider>();
        characterAnimation = GetComponent<CharacterAnimation>();
        weaponController = GetComponent<WeaponController>();
        transmitter = FindObjectOfType<Transmitter>();
	}
	
	void Update ()
    {
        if (!GameController.instance.isGamePaused)
        {
            
            string dashInputName = player.GetPlayerInputPrefix() + "Dash";
            if (Input.GetAxis(dashInputName) < InputThreshold && isDashPressed)
                isDashPressed = false;
            else if (Input.GetAxis(dashInputName) > InputThreshold && !isDashPressed)
            {
                isDashPressed = true;
                if (dashReloadTime + dashReloadBonusFactor <= 0)
                {
                    body.AddForce(player.GetMovementDirection() * dashForce, ForceMode.VelocityChange);
                    dashReloadTime = dashReloadDuration;
                    characterAnimation.Dash();
                }
            }
            if (dashReloadTime > 0)
                dashReloadTime -= Time.deltaTime;
    		
            if (player.isOnGround && Input.GetButtonDown(player.GetPlayerInputPrefix() + "Jump"))
            {
                body.AddForce(transform.up * jumpForce.y + player.GetMovementDirection() * jumpForce.x, ForceMode.VelocityChange);
                characterAnimation.Jump();
            }

            string meleeInputName = player.GetPlayerInputPrefix() + "Melee";
            if (meleeReloadTime > 0)
                meleeReloadTime -= Time.deltaTime;
            if (Input.GetButton(meleeInputName) && isInRangeOfTransmitter)
            {
                transmitter.arePlayersChanneling[player.playerIndex] = true;
            }
            else
            {
                transmitter.arePlayersChanneling[player.playerIndex] = false;
            }

            if (meleeReloadTime <= 0 && Input.GetButtonDown(meleeInputName))
            {
                List<HealthController> entitiesAtRange = meleeHitbox.GetEntitiesAtRange();
                for (int i = 0; i < entitiesAtRange.Count; i++)
                {
                    entitiesAtRange[i].GetComponentInChildren<Animator>().SetTrigger("Hit");
                    Vector3 direction = entitiesAtRange[i].transform.position - transform.position;
                    direction.y = 0;
                    direction = direction.normalized;
                    entitiesAtRange[i].GetComponent<Rigidbody>().AddForce(Vector3.up * meleePushback.y + direction * meleePushback.x, ForceMode.Impulse);
                    entitiesAtRange[i].MeleeHit(meleeDamage);
					if (entitiesAtRange [i].GetComponent<WeaponController> ().magazine.Count > 0) {
						weaponController.StealWeapon (entitiesAtRange [i].GetComponent<WeaponController> ());
					}
                }
                meleeReloadTime = meleeReloadDuration;
                characterAnimation.Attack();
            }
        }
	}
}
