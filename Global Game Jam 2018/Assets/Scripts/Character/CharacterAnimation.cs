using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    private Animator animator;
    private PlayerController player;

	void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<PlayerController>();
	}

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Dash()
    {
        animator.SetTrigger("Dash");
    }

    public void SetSpeedRatio(float ratio)
    {
        animator.SetFloat("Blend", Mathf.Clamp(ratio, 0, 1));
    }

    public void Attack()
    {
        animator.SetTrigger("cac");
    }
	
	void Update ()
    {
        animator.SetFloat("target angle", Vector3.SignedAngle(player.GetWeaponDirection(), player.GetMovementDirection(), Vector3.up) / 180);
	}
}
