using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
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

	public void Hit(Ammunition ammo)
	{
//		Debug.Log ("Hit");
		currentHP = (int) Mathf.Round(Mathf.Max(currentHP - ammo.behavior.damage * bonusFactor, 0));
		slider.value = currentHP;
		if (currentHP == 0)
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
		GetComponent<PlayerController> ().enabled = false;
		GetComponent<WeaponController> ().enabled = false;
		GetComponent<ActionController> ().enabled = false;
		GetComponent<HealthController> ().enabled = false;
		GetComponent<Collider> ().enabled = false;
		DestroyObject (GetComponent<Rigidbody> ());
	}
}
