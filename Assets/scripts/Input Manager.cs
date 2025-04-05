using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputAction MoveAction;
    [SerializeField] InputAction WASD;
    Vector2 move2;
    Vector2 move;

    [SerializeField] Rigidbody2D rigidbody2d;
     float speed = 5.0f;

    public Vector2 moveDirection = new Vector2(1, 0);

     Vector2 initialPosition;
     Vector2 finalPosition;


    void Start()
    {
        MoveAction.Enable();
        WASD.Enable();
    }
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        move2 = WASD.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        if (!Mathf.Approximately(move2.x, 0.0f) || !Mathf.Approximately(move2.y, 0.0f))
        {
            moveDirection.Set(move2.x, move2.y);
            moveDirection.Normalize();
            move = move2;
        }

    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }


}