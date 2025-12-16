using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsciladorDrone : MonoBehaviour
{
    [SerializeField] Vector3 posInicial;
    void Start()
    {
        posInicial = transform.position;
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }
}
