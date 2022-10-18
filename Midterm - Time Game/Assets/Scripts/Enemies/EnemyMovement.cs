using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int speed;

    public Rigidbody2D rb;

    void Start()
    {
        speed = 6;
    }

    void Update()
    {
        //This code could be used to make moving platforms hmm
        if(transform.position.x > -1)
        {
            speed = -6;
        }
        else if (transform.position.x < -11)
        {
            speed = 6;
        }
        transform.Translate(Time.deltaTime * speed, 0, 0);
    }
}
