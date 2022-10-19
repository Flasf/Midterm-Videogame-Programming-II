using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;

    [SerializeField] private GameObject menuPausa;

    private bool juegoPausado = false;

    private PlayerInput playerInput; //this is to refence the InputActions. The Type and Name can change depending on the InputAction map's name itself, i.e. a reference to an InputAction named PlayerControls would be "private PlayerControls playerControls" or at least this is what I could infer from https://youtu.be/3zEpfMbE30s?t=221 and https://youtu.be/Yjee_e4fICc?t=701
    private InputAction menuPause;

    private void Awake()
    {
        playerInput = new PlayerInput();
        
    }
    
    private void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (juegoPausado) 
            { 
                Reanudar(); 
            }
            else
            {
                Pausa();
            }

        }*/
    }

    private void OnEnable()
    {
        menuPause = playerInput.UI.Pause;
        menuPause.Enable();

        menuPause.performed += Pausar_NIS;
    }

    private void OnDisable()
    {
        menuPause.Disable();
    }
    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        AudioListener.pause = true;

    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        AudioListener.pause = false;
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void Pausar_NIS(InputAction.CallbackContext context)//NIS: new input system
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            Pausa();//I know it's weird, but that's how the video(first link)... made it.
        }
        else
        {
            Reanudar();
        }
    }
}
