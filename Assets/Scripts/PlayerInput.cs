using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject colliderCheckPreview = null;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Actions actions;
    private float speed = 5f;
    private Vector2 lastMovement = Vector2.right;
    private Vector2 movement = Vector2.zero;
    private float interactCheckDistance = 1f;
    private bool lockMovement = false;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.RegisterInput();
    }

    private void OnDestroy()
    {
        this.UnregisterInput();
    }

    private void RegisterInput()
    {
        this.actions = new Actions();
        this.actions.Player.Primary.performed += Primary;
        this.actions.Player.Secondary.performed += Secondary;
        this.actions.Player.Move.performed += Walk;
        this.actions.Player.Move.canceled += Walk;
        this.actions.Player.Enable();
    }

    private void UnregisterInput()
    {
        this.actions.Player.Primary.performed -= Primary;
        this.actions.Player.Secondary.performed -= Secondary;
        this.actions.Player.Move.performed -= Walk;
        this.actions.Player.Move.canceled -= Walk;
        this.actions.Player.Disable();
    }

    private void FixedUpdate()
    {
        this.rb.velocity = this.movement * this.speed;
    }

    private void Walk(InputAction.CallbackContext context)
    {
        this.movement = context.ReadValue<Vector2>();
        if (this.movement.sqrMagnitude > 0f)
        {
            this.movement.Normalize();
            this.colliderCheckPreview.transform.position = this.transform.position + (Vector3) this.movement;
            if (this.movement.x != 0)
            {
                this.lastMovement = this.movement;
                this.sprite.flipX = this.lastMovement.x < 0;
            }
        }
        else
        {
            this.rb.velocity = Vector2.zero;
        }
    }
    
    private void Primary(InputAction.CallbackContext context)
    {
        Vector2 direction = Vector2.zero;
        if (this.movement.sqrMagnitude > 0)
        {
            direction = this.movement;
        }
        else
        {
            direction = this.lastMovement;
        }

        //@TODO layermask?
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, direction, this.interactCheckDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                hit.collider.gameObject.GetComponent<Door>().Toggle();
                continue;
            }
            if (hit.collider.gameObject.CompareTag("Terminal"))
            {
                hit.collider.gameObject.GetComponent<Terminal>().Enable(this);
            }
        }
    }

    private void Secondary(InputAction.CallbackContext context)
    {
    }

    public void Lock()
    {
        this.movement = Vector2.zero;
        this.lockMovement = true;
        this.UnregisterInput();
    }

    public void Unlock()
    {
        this.lockMovement = false;
        this.RegisterInput();
    }
}
