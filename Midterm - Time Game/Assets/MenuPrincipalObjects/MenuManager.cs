using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void BotonQuit()
    {
        Debug.Log("Quitamos la aplicacion");
        Application.Quit();
    }

    public void Juego1()
    {
        SceneManager.LoadScene("Dino Run");
    }

    public void Juego2()
    {
        SceneManager.LoadScene("FlappyBird - Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu Principal");
    }

}