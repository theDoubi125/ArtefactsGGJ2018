using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInitialiser : MonoBehaviour
{
	public List<Ammunition> ammunitions;
	public int initialAmmo = 3;
	public PlayerController[] players;

	public List<GameObject> ammoObjects;

	// Use this for initialization
	void Awake ()
	{
		foreach (Ammunition ammo in ammunitions) {
			GameObject tmp = new GameObject ();
			tmp.name = ammo.name;
			tmp.AddComponent (ammo.GetType());
			ammoObjects.Add (tmp);
		}
	}

	void Start ()
	{
		players = FindObjectsOfType<PlayerController> ();
		foreach (PlayerController player in players)
			Debug.Log (player.gameObject.name);
		
		for (int i = 0; i < initialAmmo; i++) {
			int rdm = Random.Range (0, ammunitions.Count);
			foreach (PlayerController player in players) {
				WeaponController weapon = player.GetComponent<WeaponController> ();
				GameObject tmp = Instantiate (ammoObjects [rdm]);
				tmp.transform.SetParent (player.transform);
				weapon.magazine.Add (tmp);
			}
		}
	}
}