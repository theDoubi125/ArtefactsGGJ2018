using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform throwProjectilePrefab;
	public Transform attackProjectilePrefab;
	public int clipAmmo = 5;
	private int ammoLeft = 0;

    PlayerController player;
    Inventory inventory;

    private bool isThrowPressed = false;
    private bool isAttackPressed = false;
    public float InputThreshold = 0.5f;

    public float throwDistance = 20;

    void Start()
    {
        ammoLeft = clipAmmo;
	}

    public delegate void WeaponEvent();
    public WeaponEvent AttackPressed;
    public WeaponEvent AttackReleased;
    public WeaponEvent ThrowPressed;
    public WeaponEvent ThrowReleased;

    void OnEnable()
    {
		ammoLeft = clipAmmo;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        player = GetComponentInParent<PlayerController>();
        inventory = GetComponentInParent<Inventory>();
        player.OnInputPressed += OnInputPressed;
        player.OnInputReleased += OnInputReleased;
    }

    void OnDisable()
    {
        player.OnInputPressed -= OnInputPressed;
        player.OnInputReleased -= OnInputReleased;
    }

    private void OnInputPressed(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Attack:
                if (AttackPressed != null)
                    AttackPressed();
                break;
            case InputType.Throw:
                if (ThrowPressed != null)
                    ThrowPressed();
                break;
        }
    }

    private void OnInputReleased(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Attack:
                if(AttackReleased != null)
                    AttackReleased();
                break;
            case InputType.Throw:
                if (ThrowReleased != null)
                    ThrowReleased();
                break;
        }
    }

    public void Throw()
    {
        Transform projectileTransform = Instantiate<Transform>(throwProjectilePrefab);
        projectileTransform.position = transform.position + player.GetWeaponDirection().normalized * throwDistance;
        projectileTransform.rotation = Quaternion.LookRotation(player.GetWeaponDirection());
        projectileTransform.GetComponentInChildren<Inventory>().AddWeapon(this);
        inventory.UpdateWeapons();
    }

    public void Attack()
    {
        Transform projectileTransform = Instantiate<Transform>(attackProjectilePrefab);
        projectileTransform.position = transform.position + player.GetWeaponDirection().normalized * throwDistance;
        projectileTransform.rotation = Quaternion.LookRotation(player.GetWeaponDirection());
    }
}
