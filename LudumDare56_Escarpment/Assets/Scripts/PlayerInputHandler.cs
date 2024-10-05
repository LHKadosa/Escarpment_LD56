using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    //Components
    PlayerMovement PlayerMovement;

    // Events
    public delegate void PauseKeyPressed();
    public static event PauseKeyPressed OnPausePressed;

    void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        //PlayerMovement.SetInputVector(inputVector);

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            OnPausePressed?.Invoke();
        }
    }
}
