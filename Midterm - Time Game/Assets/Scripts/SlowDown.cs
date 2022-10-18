using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public GameObject dimBackground;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = .2f;
            dimBackground.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Time.timeScale = 1;
            dimBackground.SetActive(false);
        }
    }
}
