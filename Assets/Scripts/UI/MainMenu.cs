using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject centroDatos;
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject menuNiveles;

    public void OpenCentroDatos()
    {
        mainMenu.SetActive(false);
        centroDatos.SetActive(true);
        menuNiveles.SetActive(false);
    }

    public void OpenMainMenu()
    {
        centroDatos.SetActive(false);
        mainMenu.SetActive(true);
        menuNiveles.SetActive(false);
        
    }

    public void OpenMenuNiveles()
    {
        mainMenu.SetActive(false);
        centroDatos.SetActive(false);
        menuNiveles.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Creditos()
    {
        SceneManager.LoadScene(6);
    }
}
