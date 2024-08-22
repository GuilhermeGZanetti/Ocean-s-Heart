using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class BattleBoat: MonoBehaviour
{
    public BoatStats boatStats;
    

    public float sailStatus = 0.0f; // 0.0 to 1.0
    public float rudderPosition = 0.0f; // -1.0 to 1.0

    public float baseForce = 1.0f;
    public float rotationSpeed = 2.0f; // Speed at which the boat rotates towards its target direction
    public float rudderEffect = 6f;



    private Rigidbody2D rb;

    void Start(){
        //Based on the base speed, weight and max weight, calculate speed
        boatStats.speed = boatStats.baseSpeed + (boatStats.maxWeight - boatStats.weight)/(2*boatStats.maxWeight);

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // Apply force depending on sail status and rudder position
        float magnitude = boatStats.speed * sailStatus * baseForce;
        float angle = (float)(rudderPosition * (rudderEffect * Math.PI / 180));
        Vector2 force = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * magnitude;
        rb.AddRelativeForce(force);

        // Rotate the player's ship to face the direction of movement using rigidbody
        RotateToMovementDirection();
    }

    void RotateToMovementDirection()
    {
        // Get the current velocity vector of the Rigidbody2D
        Vector2 velocity = rb.velocity;

        // If the boat is moving, calculate the rotation angle and apply it
        if (velocity.magnitude > 0.01f) // Avoid rotation when velocity is near zero
        {
            float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f; // Convert to degrees and adjust angle
            float currentAngle = rb.rotation;

            // Gradually rotate towards the target angle using Mathf.LerpAngle for smooth rotation
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);

            rb.rotation = newAngle; // Set the Rigidbody2D's rotation
        }
    }
}
