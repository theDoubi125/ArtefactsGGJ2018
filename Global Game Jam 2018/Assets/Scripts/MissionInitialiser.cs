using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionInitialiser : MonoBehaviour
{
	public List<Ammunition> ammunitions;
	public int initialAmmo = 3;
	public PlayerController[] players;
	public GameObject[] hudPlayers;
	public Sprite[] behaviorSprites;
	public Sprite[] bonusSprites;
	public Sprite[] lvlSprites;
	public Color[] colorStates;

	public List<GameObject> ammoObjects;

	// Use this for initialization
	void Awake ()
	{
		foreach (Ammunition ammo in ammunitions) {
			GameObject tmp = new GameObject ();
			tmp.name = ammo.name;
			tmp.AddComponent<Ammunition> ();
			Ammunition comp = tmp.GetComponent<Ammunition> ();
			comp.behaviorchoice = ammo.behaviorchoice;
			comp.bonuschoice = ammo.bonuschoice;

			switch (ammo.behaviorchoice) {
			case Ammunition.behaviorEnum.Base:
				comp.behaviorSprite = behaviorSprites [0];
				break;
			case Ammunition.behaviorEnum.Bell:
				comp.behaviorSprite = behaviorSprites [1];
				break;
			case Ammunition.behaviorEnum.Rebound:
				comp.behaviorSprite = behaviorSprites [2];
				break;
			case Ammunition.behaviorEnum.Sniper:
				comp.behaviorSprite = behaviorSprites [3];
				break;
			default:
				break;
			}

			switch (ammo.bonuschoice) {
			case Ammunition.bonusEnum.Armor2:
				comp.bonusSpritePositive = bonusSprites[0];
				comp.bonusSpriteNegative = bonusSprites[0];
				comp.lvlSprite = lvlSprites[1];
				break;
			case Ammunition.bonusEnum.Armor:
				comp.bonusSpritePositive = bonusSprites[0];
				comp.bonusSpriteNegative = bonusSprites[0];
				comp.lvlSprite = lvlSprites[0];
				break;
			case Ammunition.bonusEnum.DashReload:
				comp.bonusSpritePositive = bonusSprites[3];
				comp.bonusSpriteNegative = bonusSprites[4];
				comp.lvlSprite = lvlSprites[0];
				break;
			case Ammunition.bonusEnum.Health2:
				comp.bonusSpritePositive = bonusSprites[1];
				comp.bonusSpriteNegative = bonusSprites[2];
				comp.lvlSprite = lvlSprites[1];
				break;
			case Ammunition.bonusEnum.Health:
				comp.bonusSpritePositive = bonusSprites[1];
				comp.bonusSpriteNegative = bonusSprites[2];
				comp.lvlSprite = lvlSprites[0];
				break;
			case Ammunition.bonusEnum.Speed2:
				comp.bonusSpritePositive = bonusSprites[3];
				comp.bonusSpriteNegative = bonusSprites[4];
				comp.lvlSprite = lvlSprites[1];
				break;
			case Ammunition.bonusEnum.Speed:
				comp.bonusSpritePositive = bonusSprites[3];
				comp.bonusSpriteNegative = bonusSprites[4];
				comp.lvlSprite = lvlSprites[0];
				break;
			case Ammunition.bonusEnum.Upload:
				comp.bonusSpritePositive = bonusSprites[5];
				comp.bonusSpriteNegative = bonusSprites[5];
				comp.lvlSprite = lvlSprites[0];
				break;
				
			}
			ammoObjects.Add (tmp);
		}
		players = FindObjectsOfType<PlayerController> ();
		foreach (PlayerController player in players) {
			hudPlayers [player.playerIndex].SetActive (true);
			player.personnalHUD = hudPlayers [player.playerIndex];
		}
	}

	void Start ()
	{
		for (int i = 0; i < initialAmmo; i++) {
			int rdm = Random.Range (0, ammunitions.Count);
			foreach (PlayerController player in players) {
				GameObject tmp = Instantiate (ammoObjects [rdm]);
				tmp.transform.SetParent (player.transform);
				WeaponController weapon = player.GetComponent<WeaponController> ();
				weapon.magazine.Add (tmp);
				weapon.AddHUDAmmo (tmp.GetComponent<Ammunition>());
			}
		}
	}
}