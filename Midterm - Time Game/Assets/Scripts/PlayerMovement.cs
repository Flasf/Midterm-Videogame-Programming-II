using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputAsset;
    public Rigidbody2D rb;
    public float moveSpeed = 120;
    public float jumpForce = 120;
    public CharacterController2D controller; 

    public LayerMask layerGround;
    public float distRayCast = 0.6f;
    public bool isOnGround;

    private bool canJump = true;

    private void Awake()
    {
        canJump = true;
    }


    // Update is called once per frame
    void Update()
    {
        CheckIsOnGround();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        //Jump();
    }
    private void Move()
    {
        rb.velocity = new Vector3(GetAxisDirectAsset("Move").x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, 0);
    }
    private Vector2 GetAxisDirectAsset(string action)
    {
        return inputAsset.FindActionMap("Player").FindAction(action).ReadValue<Vector2>();
    }

    private void CheckIsOnGround()
    {
        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, distRayCast, layerGround.value); //Tira un raycast al suelo para saber si está en el piso o no
        if (isOnGround && canJump == false)
        {
            canJump = true;
        }
    }

    private void Jump()
    {
        if (isOnGround)
        {
            
            if (GetKeyDown("Jump") && canJump)
            {
                canJump = false;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            }
        }
    }
    private void OnDrawGizmos() //para mostrar el raycast de CheckIsOnGround
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distRayCast);   
    }

    //Parecido al Anterior Input System
    private bool GetKeyDown(string action)
    {
        return inputAsset.FindActionMap("Player").FindAction(action).WasPerformedThisFrame();
    }
    
    private bool GetKey(string action)
    {
        return inputAsset.FindActionMap("Player").FindAction(action).IsPressed();
    }

    private bool GetKeyUp(string action)
    {
        return inputAsset.FindActionMap("Player").FindAction(action).WasReleasedThisFrame();
    }


}
