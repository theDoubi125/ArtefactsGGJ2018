using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    Animation anim;

	void Start ()
    {
	}
	
    public void Show(int player)
    {
        GetComponent<Animator>().SetTrigger("Show Menu");
	}

    public void ShowFailure()
    {
        GetComponent<Animator>().SetTrigger("Show Menu");
    }
}
