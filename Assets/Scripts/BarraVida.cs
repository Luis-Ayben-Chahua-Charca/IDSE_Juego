using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    [SerializeField] private ControlDeNave controlDeNave;
    private float vidaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        //controlDeNave = GameObject.Find("Nave").GetComponent<ControlDeNave>();
        vidaMaxima = controlDeNave.vida;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = controlDeNave.vida / vidaMaxima;
    }
}
