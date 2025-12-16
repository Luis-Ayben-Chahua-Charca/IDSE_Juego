using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject centroDatos;
    [SerializeField] public GameObject mainMenu;

    public void OpenCentroDatos()
    {
        mainMenu.SetActive(false);
        centroDatos.SetActive(true);
    }

    public void OpenMainMenu()
    {
        centroDatos.SetActive(false);
        mainMenu.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
