using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private float speed = 5f;
    [SerializeField] private LayerMask collisionLayers = 0;
    [SerializeField] private float dmg = 1;

    public void Fire(Vector2 direction)
    {
        direction.Normalize();
        this.transform.rotation = Quaternion.Euler(0f, 0f, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg - 90f);
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.velocity = direction * this.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.collisionLayers != (this.collisionLayers | (1 << other.gameObject.layer))) return;
        Health health = other.gameObject.GetComponent<Health>();
        if(health) { health.Damage(this.dmg); }
        Debug.Log(other.gameObject);
        Destroy(this.gameObject);
    }
}
