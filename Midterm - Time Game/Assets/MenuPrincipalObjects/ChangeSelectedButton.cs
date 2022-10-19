using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class ChangeSelectedButton : MonoBehaviour
{
    public GameObject playButton, returnButton;
    private EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
        //playButton = GetComponent<GameObject>();
        //returnButton = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        eventSystem.firstSelectedGameObject = playButton;//couldn't implement this because of NullReferenceException: Object not set to an instance of an object. Although I already "connected" it through the Unity Editor...
    }
    private void OnDisable()
    {
        eventSystem.firstSelectedGameObject = returnButton;//same NullReferenceExecption here .-.
    }
}
