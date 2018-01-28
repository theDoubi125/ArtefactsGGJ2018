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

    void Start ()
    {
        player = GetComponent<PlayerController>();
        body = GetComponent<Rigidbody>();
        meleeHitbox = GetComponentInChildren<MeleeAttackCollider>();
	}
	
	void Update ()
    {
        if (!GameController.instance.isGamePaused)
        {
            
            string dashInputName = player.GetPlayerInputPrefix() + "Dash";
            if (dashReloadTime + dashReloadBonusFactor <= 0 && Input.GetButtonDown(dashInputName))
            {
                body.AddForce(player.GetMovementDirection() * dashForce, ForceMode.VelocityChange);
                dashReloadTime = dashReloadDuration;
                GetComponent<CharacterAnimation>().Dash();
            }
            if (dashReloadTime > 0)
                dashReloadTime -= Time.deltaTime;
    		
            if (player.isOnGround && Input.GetButtonDown(player.GetPlayerInputPrefix() + "Jump"))
            {
                body.AddForce(transform.up * jumpForce.y + player.GetMovementDirection() * jumpForce.x, ForceMode.VelocityChange);
                GetComponent<CharacterAnimation>().Jump();
            }

            string meleeInputName = player.GetPlayerInputPrefix() + "Melee";
            if (meleeReloadTime > 0)
                meleeReloadTime -= Time.deltaTime;
            if (Input.GetButton(meleeInputName) && isInRangeOfTransmitter)
            {
                FindObjectOfType<Transmitter>().arePlayersChanneling[player.playerIndex] = true;
            }
            else
            {
                FindObjectOfType<Transmitter>().arePlayersChanneling[player.playerIndex] = false;
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
                    GetComponent<WeaponController>().StealWeapon(entitiesAtRange[i].GetComponent<WeaponController>());
                }
                meleeReloadTime = meleeReloadDuration;
                GetComponent<CharacterAnimation>().Attack();
            }
        }
	}
}
