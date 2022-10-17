using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

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

        Flip();

        ChangeAnimationIfActionIsMade();
        
        

        /* CODE NOT NEEDED, BUT FUNCTIONAL IN CASE SOMETHING HAPPENS TO NEW INPUT METHOD
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
}
