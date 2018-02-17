using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	public Transform spawner;
	public int currentHP;
	public int maxHP;
	public int minHP;
	public float bonusFactor = 1f;
	private Slider slider;

	// Use this for initialization
	void Start ()
	{
		currentHP = maxHP;
		slider = GetComponent<PlayerController> ().personnalHUD.GetComponentInChildren<Slider> ();
		slider.maxValue = maxHP;
		slider.value = currentHP;
	}

	public void Damage(int damage)
	{
		currentHP -= damage;
        if(slider != null)
		    slider.value = currentHP;
		if (currentHP <= 0)
		{
			Death ();
		}
	}

	public void MeleeHit(int meleeDamage)
	{
		currentHP -= meleeDamage;
		slider.value = currentHP;
		if (currentHP == 0)
		{
			Death ();
		}
	}

	public void Death ()
	{
		currentHP = maxHP;
        GameController.instance.GetComponent<RespawnManager>().AddPlayer(transform, 3);
	}
}
