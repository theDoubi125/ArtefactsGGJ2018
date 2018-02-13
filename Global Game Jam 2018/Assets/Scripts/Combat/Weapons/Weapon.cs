using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAction
{
    Attack,
    Throw
}

public class Weapon : MonoBehaviour
{
    PlayerController player;
    

    void Start()
    {

	}

    public delegate void WeaponEvent();
    public WeaponEvent AttackPressed;
    public WeaponEvent AttackReleased;
    public WeaponEvent ThrowPressed;
    public WeaponEvent ThrowReleased;

    void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        player = GetComponentInParent<PlayerController>();
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

    public void BindToAction(WeaponAction action, WeaponEvent pressedAction, WeaponEvent releasedAction)
    {
        switch(action)
        {
            case WeaponAction.Attack:
                AttackPressed += pressedAction;
                AttackReleased += releasedAction;
                break;
            case WeaponAction.Throw:
                ThrowPressed += pressedAction;
                ThrowReleased += releasedAction;
                break;
        }
    }

    public void UnbindAction(WeaponAction action, WeaponEvent pressedAction, WeaponEvent releasedAction)
    {
        switch (action)
        {
            case WeaponAction.Attack:
                AttackPressed -= pressedAction;
                AttackReleased -= releasedAction;
                break;
            case WeaponAction.Throw:
                ThrowPressed -= pressedAction;
                ThrowReleased -= releasedAction;
                break;
        }
    }
}
