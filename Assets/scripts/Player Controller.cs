using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2d;
    float speed = 5.0f;
    [SerializeField] InputManager inputManager;
    Vector2 moveDirection;
    Vector2 move;
    Vector2 move2;

    
    void Update()
    {
        move = inputManager.GetMove();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
            inputManager.SetMove(move);
        }

    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public Vector2 GetMoveDirection() => moveDirection;
}
