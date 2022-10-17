using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject collisionGameObject = collision.gameObject;

        if (other.gameObject.CompareTag("Player"))
        {
            LoadLevel();
        }
    }
    void LoadLevel()
    {
        SceneManager.LoadScene("LevelOne");
    }
    
}
