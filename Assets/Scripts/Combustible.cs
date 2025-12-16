using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraCombustible : MonoBehaviour
{
    public Image rellenoBarraCombustible;
    [SerializeField] private ControlDeNave controlDeNave;
    private float combustibleMaximo;
    // Start is called before the first frame update
    void Start()
    {
        //controlDeNave = GameObject.Find("Nave").GetComponent<ControlDeNave>();
        combustibleMaximo = controlDeNave.combustible;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraCombustible.fillAmount = controlDeNave.combustible / combustibleMaximo;
    }
}
