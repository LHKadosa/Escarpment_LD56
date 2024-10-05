using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tank Movment setting")]
    public float accelerationFactor = 30.0f;
    public float turnfactor = 3.5f;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    //Components
    Rigidbody2D tankBody;

    private void Awake()
    {
        tankBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //Base force for the Tank's engine
        Vector2 engineForceVector = transform.up * accelerationFactor * accelerationInput;

        //Apply the force to move the tank forward
        tankBody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    { 
        //Update the rotation the Tank faces
        rotationAngle -= steeringInput * turnfactor;

        //Apply steering by rotating the Tank
        tankBody.MoveRotation(rotationAngle);
    }

    //Input to vector
    public void SetInputVector(Vector2 inputVector)
    { 
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
