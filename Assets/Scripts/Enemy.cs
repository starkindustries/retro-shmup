﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, Damageable, Scorable
{
    public int health;
    public int points;
    public float speed;
    public float rotationSpeed;
    
    private bool isDead = false;
    private Rigidbody2D rb;
    private int currentTargetIndex;
    private FlightPath flightPath;
    private float minDistance = 1f;

    #region Start & Update
    // Start is called before the first frame update
    private void Start()
    {
        Health = health;
        Points = points;
        Scoreboard = FindObjectOfType<Scoreboard>();
        rb = GetComponent<Rigidbody2D>();

        // Set starting position
        transform.position = flightPath.points[0].position;
        
        currentTargetIndex = 1;

        // Flip the enemy
        transform.Rotate(0f, 180f, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // For Physics!
    private void FixedUpdate()
    {
        if(!isDead)
        {
            float distance = Vector2.Distance(flightPath.points[currentTargetIndex].position, rb.position);
            if ((distance < minDistance) && (currentTargetIndex < flightPath.points.Length - 1))
            {
                currentTargetIndex++;
            }

            Vector2 direction = (Vector2)flightPath.points[currentTargetIndex].position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            rb.angularVelocity = -rotateAmount * rotationSpeed; // float        
            rb.velocity = transform.right * speed; // vector2
        }
    }
    #endregion

    public void SetFlightPath(FlightPath path)
    {
        flightPath = path;
    }

    private void Die()
    {
        // make explosion
        isDead = true;
        rb.gravityScale = 1f;
        Score();
    }

    #region Damageable Interface
    // ************************
    // Damageable Interface
    // ************************
    public int Health { get; set; }
    
    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            Die();
        }
    }
    #endregion

    #region Scorable Interface
    // ************************
    // Scorable Interface
    // ************************
    public int Points { get; set; }
    public Scoreboard Scoreboard { get; set; }

    public void Score()
    {
        Scoreboard.AddPoints(Points);
    }
    #endregion
}