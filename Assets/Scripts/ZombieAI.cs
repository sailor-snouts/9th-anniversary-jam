using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private float speed = 3f;
    
    private Seeker seeker;
    public Transform target;
    private Path path;
    private int currentWayPoint;
    private bool reachedDestination = false;
    private float nextWayPointDistance = 0.5f;
    
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.seeker = GetComponent<Seeker>();
        this.rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (!this.seeker.IsDone()) return;
        this.seeker.StartPath(this.transform.position, this.target.position, OnPathComplete);
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
        Vector2 direction = (path.vectorPath[currentWayPoint] - this.transform.position).normalized;
        this.rb.velocity = direction * this.speed;
        this.sprite.flipX = direction.x > 0;

        if (Vector2.Distance(this.transform.position, path.vectorPath[this.currentWayPoint]) < this.nextWayPointDistance)
        {
            this.currentWayPoint++;
        }
    }
}
