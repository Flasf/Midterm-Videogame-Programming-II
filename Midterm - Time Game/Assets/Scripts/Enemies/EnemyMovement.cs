using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int speed;

    public Rigidbody2D rb;

    private bool facingRight = true;


    void Start()
    {
        speed = 6;
    }

    void Update()
    {
        //This code could be used to make moving platforms hmm
        if (transform.position.x > 43)
        {
            speed = -6;
            if (!facingRight) { Flip(); }
        }
        else if (transform.position.x < 21)
        {
            speed = 6;
            if (!facingRight) { Flip(); }
        }
        transform.Translate(Time.deltaTime * speed, 0, 0);
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }
}

