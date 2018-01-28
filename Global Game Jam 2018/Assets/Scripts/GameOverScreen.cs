using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    Animation anim;
    Animator animator;

	void Start ()
    {
        animator = GetComponent<Animator>();
	}

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool keyDown = Input.anyKeyDown;
        if (keyDown && stateInfo.IsTag("End"))
        {
            GameController.instance.controllers.Clear();
            GameController.instance.isGamePaused = false;
            SceneManager.LoadScene("Character select screen");
        }
    }
	
    public void Show(int player)
    {
        animator.SetTrigger("Show Menu");
	}

    public void ShowFailure()
    {
        animator.SetTrigger("Show Menu");
    }
}
