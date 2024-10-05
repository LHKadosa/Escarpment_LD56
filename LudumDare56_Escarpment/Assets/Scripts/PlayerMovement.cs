using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tank Movment setting")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 5f;
    public float turnfactor = 2f;
    public float maxSpeed = 20;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    float velocityVsUp = 0;

    //Components
    Rigidbody2D tankBody;

    private void Awake()
    {
        tankBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        StopSidewaysSliding();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //Limiting movement speed of the Tank
        velocityVsUp = Vector2.Dot(transform.up, tankBody.velocity);
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }
        //Backwars limit being less then forward limit
        if (velocityVsUp < -maxSpeed * 0.5 && accelerationInput < 0)
        {
            return;
        }
        //Sideways movement limit
        if (tankBody.velocity.magnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //Drag to slow down the movement of the Tank when no movement input is given to it
        if (accelerationInput == 0)
        {
            tankBody.drag = Mathf.Lerp(tankBody.drag, 5.4f, Time.fixedDeltaTime * 1.2f);
        }
        else tankBody.drag = 0;

        //Base force for the Tank's engine
        Vector2 engineForceVector = transform.up * accelerationFactor * accelerationInput;

        //Apply the force to move the tank forward
        tankBody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limit when the player can turn
        float minSpeedBeforeAllowTurnFactor = (tankBody.velocity.magnitude / 8);
        minSpeedBeforeAllowTurnFactor = Mathf.Clamp01(minSpeedBeforeAllowTurnFactor);

        //Update the rotation the Tank faces
        rotationAngle -= steeringInput * turnfactor * minSpeedBeforeAllowTurnFactor;

        //Apply steering by rotating the Tank
        tankBody.MoveRotation(rotationAngle);
    }

    //Input to vector
    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void StopSidewaysSliding()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(tankBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(tankBody.velocity, transform.right);

        tankBody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}