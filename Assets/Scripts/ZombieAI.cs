using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    
    
    private Seeker seeker;
    public Transform target;
    private Path path;
    private int currentWayPoint;
    private bool reachedDestination = false;
    private float nextWayPointDistance = 0.5f;

    private float speed = 3f;
    private Health health = null;
    private float damage = 100f;
    
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.seeker = GetComponent<Seeker>();
        this.rb = GetComponent<Rigidbody2D>();
        this.health = GetComponent<Health>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.Register();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void OnDestroy()
    {
        this.Unregister();
    }

    private void Register()
    {
        this.health.onDeath += this.OnDeath;
    }

    private void Unregister()
    {
        this.health.onDeath -= this.OnDeath;
    }

    private void OnDeath()
    {
        Destroy(this.gameObject);
    }

    private void UpdatePath()
    {
        if (!this.seeker.IsDone()) return;
        this.seeker.StartPath(this.transform.position + (Vector3) this.circleCollider.offset, this.target.position, OnPathComplete);
    }
    
    private void OnPathComplete(Path p)
    {
        if (p.error)
        {
            return;
        }

        this.path = p;
        this.currentWayPoint = 0;
    }

    private void FixedUpdate()
    {
        if (this.path == null)
        {
            return;
        }

        if (currentWayPoint >= this.path.vectorPath.Count)
        {
            this.reachedDestination = true;
            this.rb.velocity = Vector2.zero;
            return;
        }

        
        this.reachedDestination = false;
        Vector2 direction = (path.vectorPath[currentWayPoint] - (this.transform.position + (Vector3) this.circleCollider.offset)).normalized;
        this.rb.velocity = direction * this.speed;
        this.sprite.flipX = direction.x > 0;

        if (Vector2.Distance(this.transform.position + (Vector3) this.circleCollider.offset, path.vectorPath[this.currentWayPoint]) < this.nextWayPointDistance)
        {
            this.currentWayPoint++;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.Damage(this.damage);
        }
    }
}
