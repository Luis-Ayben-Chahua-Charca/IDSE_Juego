using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
//[ExecuteInEditMode]
public class OsciladorDrone : MonoBehaviour
{
    [SerializeField] Vector3 posInicial;
    [SerializeField] Vector3 dirDesplazamiento;
    [SerializeField] [Range(0,1)] float desplazamiento;
    [SerializeField] float periodo;
    void Start()
    {
        posInicial = transform.position;
    }
     
    // Update is called once per frame
    void Update()
    {
        if (periodo >= Mathf.Epsilon)
        {

            float ciclos = Time.time / periodo;
            print(periodo);

            float tau = Mathf.PI * 2;
            float funcionSeno = Mathf.Sin(tau * ciclos);
            desplazamiento = funcionSeno * 0.5f + 0.5f;
            print(desplazamiento);

            transform.position = posInicial + dirDesplazamiento * desplazamiento;
        }
    }
}
