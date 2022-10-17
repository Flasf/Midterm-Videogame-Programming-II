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

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //Note: serializeFiled let's us access private variables on Unity, yet it does not allow them to be used in other scripts
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer trailRenderer;

    public LayerMask layerGround;
    public float distRayCast = 0.6f;
    public bool isOnGround;
    

    private Animator playerAnimator;

    private void Awake()
    {
        
    }
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)//prevents player from moving, jumping and fliping while dashing
        {
            return;
        }
        //horizontal = Input.GetAxisRaw("Horizontal");
        //CheckIsOnGround();
        //Jump();

        Flip();

        ChangeAnimationIfActionIsMade();
        
        

        /* CODE NOT NEEDED, BUT FUNCTIONAL EITHER WAY
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpPower);
        }
        //this one allows us to jump higher by longer pressing  the button
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *0.5f);
        }*/

        

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

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

    //YT: bendux. this guy deserves heaven
    public void Jump(InputAction.CallbackContext context)//Unity new input system doesn't need to be checked in update or fixed update btw
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        //this one allows us to jump higher by longer pressing  the button
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void ActivateDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0; //resets gravity to 0
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);//transform.localScale.x means the direction player is facing
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);//this is to prevent player from dashing infinitely
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
    private void ChangeAnimationIfActionIsMade()
    {
        if (horizontal != 0f)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

        if(rb.velocity.y == 0f)
        {
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isFalling", false);
        }
        else if(rb.velocity.y > 0f)
        {
            playerAnimator.SetBool("isJumping", true);
        }
        else if(rb.velocity.y < 0f)
        {
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isFalling", true);
        }
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
    }

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
