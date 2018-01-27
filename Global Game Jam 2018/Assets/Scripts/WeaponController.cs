using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public Transform projectilePrefab;
	public List<Ammunition> magazine;
	private PlayerController player;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown(player.GetPlayerInputPrefix() + "Action1"))
		{
			if (magazine.Count > 0)
			{
				Transform projectile = Instantiate(projectilePrefab);
				projectile.position = transform.position + transform.right * 0.5f;
                projectile.rotation = transform.rotation;
				projectile.gameObject.AddComponent <Ammunition>();
				Ammunition tmp = projectile.gameObject.GetComponent<Ammunition>();
				tmp.behaviorchoice = magazine[0].behaviorchoice;
				tmp.bonuschoice = magazine[0].bonuschoice;
				tmp.PlayerId = player.playerIndex;
				magazine.RemoveAt(0);
			}
			else
			{
				//TODO : Play SFX
			}
		}
	}

	public void Hit ()
	{
		Debug.Log(player.playerIndex + " has been hit");
	}
}
