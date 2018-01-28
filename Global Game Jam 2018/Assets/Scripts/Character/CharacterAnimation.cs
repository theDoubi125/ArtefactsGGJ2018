using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    private Animator animator;

	void Start ()
    {
        animator = GetComponentInChildren<Animator>();
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
        animator.SetTrigger("CaC");
    }
	
	void Update ()
    {
		
	}
}
