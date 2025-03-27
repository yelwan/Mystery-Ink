using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputAction MoveAction;
   public Rigidbody2D rigidbody2d;
    public float speed = 3.0f;

    Vector2 move;

    Vector2 moveDirection = new Vector2(1, 0);

    void Start()
    {
        MoveAction.Enable();
    }
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();


        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
    }
        void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }


}