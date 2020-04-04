using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject colliderCheckPreview;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float speed = 5f;
    private Vector2 lastMovement;
    private Vector2 movement = Vector2.right;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
    }

    public void Walk(InputAction.CallbackContext context)
    {
        this.movement = context.ReadValue<Vector2>();
        if (this.movement.sqrMagnitude > 0f)
        {
            this.movement.Normalize();
            this.rb.velocity = this.movement * this.speed;
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
    
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Vector3 origin = this.transform.position;
            if (this.movement.sqrMagnitude > 0)
            {
                origin = (Vector3) this.movement.normalized;
            }
            else
            {
                origin += (Vector3) this.lastMovement.normalized;
            }
            
            Collider2D[] collisions = Physics2D.OverlapCircleAll(origin, 0.1f);//@TODO layermask?
            foreach (Collider2D collision in collisions)
            {
                if (collision.gameObject.CompareTag("Door"))
                {
                    collision.gameObject.GetComponent<Door>().Toggle();
                }
            }
        }
    }

    private void FixedUpdate()
    {
    }
}
