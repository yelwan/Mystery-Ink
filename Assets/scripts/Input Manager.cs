using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Code review : add functions to enable and disable inputs so that 
    // you can call them in the start and end level flows (cutscenes, etc...)
    [SerializeField] InputAction MoveAction;
    [SerializeField] InputAction E;
    Vector2 move;
    Vector2 initialPosition;
    Vector2 finalPosition;
    float ePressed = 0.0f;

    void Start()
    {
        E.Enable();
        MoveAction.Enable();
    }
    void Update()
    { 
        move = MoveAction.ReadValue<Vector2>();
        ePressed = E.ReadValue<float>();
    }
    
    public Vector2 GetMove() => move;
    public void SetMove(Vector2 _move) {
        move = _move;
     }
    public bool GetEPressed() => ePressed != 0.0f;
}