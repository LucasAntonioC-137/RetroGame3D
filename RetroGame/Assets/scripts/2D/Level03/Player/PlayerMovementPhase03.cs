using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPhase03 : PhysicsObject
{
    public float maxSpeed = 5;
    public float jumpForce = 10;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpForce;
        }
        else if(Input.GetButtonUp("Jump"))
        {
             if(velocity.y > 0) { }
            {
                velocity.y *= 0.5f;
            }
        }

        targetVelocity = move * maxSpeed;
    }
}
