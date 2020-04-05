using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private float speed = 5f;
    private Vector2 direction = Vector2.zero;

    public void Fire(Vector2 direction)
    {
        direction.Normalize();
        Quaternion.Euler(0f, 0f, -Mathf.Atan2(this.direction.x, this.direction.y) * Mathf.Rad2Deg);
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.velocity = direction * this.speed;
    }
}
