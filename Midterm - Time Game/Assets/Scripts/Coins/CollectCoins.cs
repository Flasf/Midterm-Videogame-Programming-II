using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    public AudioClip coinSound; // Assign the coin sound clip in the Inspector
    public AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = coinSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the coin sound when the player collects the coin
            
            audioSource.Play();

            // Destroy the coin when the player enters the trigger
            Destroy(gameObject);
        }
    }
}
