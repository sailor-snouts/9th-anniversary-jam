using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private Actions actions = null;
    private PlayerController player = null;
    private Vector2 direction = Vector2.zero;
    private float fireRate = 0.5f;
    
    private void Start()
    {
        this.actions = new Actions();
    }

    private void OnDestroy()
    {
        this.UnregisterInput();
    }

    private void RegisterInput()
    {
        this.actions.Turret.MouseAim.performed += MouseAim;
        this.actions.Turret.JoystickAim.performed += JoystickAim;
        this.actions.Turret.Fire.started += Fire;
        this.actions.Turret.Fire.canceled += Fire;
        this.actions.Turret.Unequip.performed += Unequip;
        this.actions.Turret.Enable();
    }

    private void UnregisterInput()
    {
        this.actions.Turret.MouseAim.performed -= MouseAim;
        this.actions.Turret.JoystickAim.performed -= JoystickAim;
        this.actions.Turret.Fire.started -= Fire;
        this.actions.Turret.Fire.canceled -= Fire;
        this.actions.Turret.Unequip.performed -= Unequip;
        this.actions.Player.Disable();
    }

    private void MouseAim(InputAction.CallbackContext context)
    {
        Vector2 dir = (Vector2) Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()) - (Vector2) this.transform.position;
        this.Aim(dir);
    }

    private void JoystickAim(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        this.Aim(dir);
    }

    private void Aim(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0f)
        {
            dir.Normalize();
            this.direction = dir;
            transform.rotation = Quaternion.Euler(0f,0f,-Mathf.Atan2(this.direction.x,this.direction.y) * Mathf.Rad2Deg);
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine("Shoot");
        }

        if (context.canceled)
        {
            StopCoroutine("Shoot");
        }
    }

    IEnumerator Shoot()
    {
        for(;;) 
        {
            Vector3 spawn = this.transform.position;
            GameObject obj = Instantiate(this.bullet, spawn, Quaternion.identity);
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.Fire(this.direction);
            yield return new WaitForSeconds(this.fireRate);
        }
    }

    public void Equip(PlayerController player)
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
