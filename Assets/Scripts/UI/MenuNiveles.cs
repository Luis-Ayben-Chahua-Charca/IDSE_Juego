using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNiveles : MonoBehaviour
{
    public void AbrirNivel1()
    {
        SceneManager.LoadScene(1);
    }
    public void AbrirNivel2()
    {
        SceneManager.LoadScene(2);
    }
    public void AbrirNivel3()
    {
        SceneManager.LoadScene(3);
    }
    public void AbrirNivel4()
    {
        SceneManager.LoadScene(4);

    }
    public void AbrirNivel5()
    {
        SceneManager.LoadScene(5);
    }
}
