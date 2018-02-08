using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    private Animator animator;
    private PlayerController player;
	public bool debug = false;

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
		float angle = Vector3.SignedAngle (player.GetWeaponDirection (), animator.transform.right, Vector3.up) / 360;
		if (angle < 0)
			angle += 1;
		animator.SetFloat ("target angle", 1-angle);
	}
}
