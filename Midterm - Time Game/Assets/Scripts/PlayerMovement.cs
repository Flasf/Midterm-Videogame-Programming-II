using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputAsset;//no
    public Rigidbody2D rb;
    public float moveSpeed = 120;
    public float jumpForce = 120;//no
    public CharacterController2D controller;//no

    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;
    private float jumpPower = 16;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;




    public LayerMask layerGround;
    public float distRayCast = 0.6f;
    public bool isOnGround;

    private bool isMoving;
    private bool canJump = true;

    private Animator playerAnimator;

    private void Awake()
    {
        canJump = true;
    }
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        //CheckIsOnGround();
        //Jump();

        Flip();

        ChangeAnimationIfActionIsMade();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpPower);
        }
        //this one allows us to jump higher by pressing longer the button
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *0.5f);
        }


    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void FixedUpdate()
        {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            //Move();
        }
    
    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void ChangeAnimationIfActionIsMade()
    {
        if (isMoving)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }
        //TODO: Buscar cómo poner el isMoving en false...
    }
    
    /*
    private void Move()
    {
        rb.velocity = new Vector3(GetAxisDirectAsset("Move").x * moveSpeed * Time.unscaledDeltaTime, rb.velocity.y, 0);
        
        isMoving = true;
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
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) //layer 6 is ground
        {
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6) //layer 6 is ground
        {
            canJump = false;
        }
    }

    /*
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
    }*/

    
}
