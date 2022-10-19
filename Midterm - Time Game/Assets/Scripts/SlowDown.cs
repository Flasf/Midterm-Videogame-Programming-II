using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlowDown : MonoBehaviour
{
    public GameObject dimBackground;

    private bool canSlowTime = true;
    private bool isSlowingTime;
    private float slowingTime_LimitTime = 1f;
    private float slowingTimeCooldown = 2f;

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
    
    public void SlowTime(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Button pressed");
            canSlowTime = true;
            Time.timeScale = .2f;
            dimBackground.SetActive(true);
            StartCoroutine(SlowTimeCoroutime(context.duration));
        }
        else if (context.canceled)
        {
            canSlowTime = false;
            Time.timeScale = 1;
            dimBackground.SetActive(false);
        }
    }
    
    private IEnumerator SlowTimeCoroutime(double time)
    {
        canSlowTime = false;
        isSlowingTime = true;
        Time.timeScale = .2f;
        dimBackground.SetActive(true);
        yield return new WaitForSeconds((float)time);//this is to prevent player from slowingTime infinitely
        Time.timeScale = 1;
        dimBackground.SetActive(false);
        isSlowingTime = false;
        yield return new WaitForSeconds(slowingTimeCooldown);
        canSlowTime = true;
        //tengo que poner StopCoroutine?
    }
}
