using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float moveSpeedMult = 5f;
    [SerializeField] Rigidbody2D myRB2D;

    void Start()
    {

    }

    void Update()
    {
        MovePlayer();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        if(moveInput.sqrMagnitude > 1f)
        {
            moveInput.Normalize();
        }

        myRB2D.velocity = moveInput*moveSpeedMult;
    }
}
