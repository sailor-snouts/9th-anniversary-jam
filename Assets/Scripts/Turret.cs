using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    private Actions actions;
    private PlayerInput player = null;

    private void Start()
    {
    }

    private void OnDestroy()
    {
        this.UnregisterInput();
    }

    private void RegisterInput()
    {
        this.actions = new Actions();
        this.actions.Turret.MouseAim.performed += MouseAim;
        this.actions.Turret.JoystickAim.performed += JoystickAim;
        this.actions.Turret.Fire.performed += Fire;
        this.actions.Turret.Unequip.performed += Unequip;
        this.actions.Turret.Enable();
    }

    private void UnregisterInput()
    {
        this.actions.Turret.MouseAim.performed -= MouseAim;
        this.actions.Turret.JoystickAim.performed -= JoystickAim;
        this.actions.Turret.Fire.performed -= Fire;
        this.actions.Turret.Unequip.performed -= Unequip;
        this.actions.Player.Disable();
    }

    private void MouseAim(InputAction.CallbackContext context)
    {
        Vector2 direction = (Vector2) Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()) - (Vector2) this.transform.position;
        this.Aim(direction);
    }

    private void JoystickAim(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        this.Aim(direction);
    }

    private void Aim(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0f)
        {
            direction.Normalize();
            transform.rotation=Quaternion.Euler(0f,0f,-Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg);
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void Equip(PlayerInput player)
    {
        this.player = player;
        this.player.Lock();
        this.RegisterInput();
    }

    private void Unequip(InputAction.CallbackContext context)
    {
        this.UnregisterInput();
        this.player.Unlock();
    }
}
