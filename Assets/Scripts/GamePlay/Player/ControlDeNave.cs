using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System;

public class ControlDeNave : MonoBehaviour
{
    Rigidbody rbody;
    [Header ("AudioSources")]
    [SerializeField] AudioSource audioSFX;
    [SerializeField] AudioSource audioPropulsion;

    private bool colisionesActivas;

    public enum EstadoJugador
    {
        Vivo,
        Muerto,
        SinCombustible,
        NivelCompleto
    }


    public EstadoJugador estadoActual = EstadoJugador.Vivo;
    private int nivelActual;

    [SerializeField] public float vida = 5.0f;
    [SerializeField] public float combustible = 10.0f;
    [SerializeField] public float consumoCombustibleSegundo = 0.2f;

    [SerializeField] private float velocidadPropulsion = 400.0f;
    [SerializeField] private float velocidadRotacion = 100.0f;

    [SerializeField] private float tiempoMuerte = 3.0f;
    [SerializeField] private float tiempoGanar = 2.0f;

    [Header ("Sonidos")]
    [SerializeField] AudioClip sonidoPropulsion;
    [SerializeField] AudioClip sonidoVictoria;
    [SerializeField] AudioClip sonidoMuerte;
    [SerializeField] AudioClip sonidoChoque;
    [SerializeField] AudioClip sonidoEnergia;
    [SerializeField] AudioClip sonidoCuracion;


    [Header ("Particulas")]
    [SerializeField] ParticleSystem partMuerte;
    [SerializeField] ParticleSystem partGanar;
    [SerializeField] ParticleSystem partPropulsion;

    [Header ("Para destruir")]
    [SerializeField] private GameObject nave;
    [SerializeField] private GameObject energia;
    // Start is called before the first frame update



    void Start()
    {
        DynamicGI.UpdateEnvironment();
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime + "seg. " + (1.0f / Time.deltaTime) + "FPS"); 
        procesarInput();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (estadoActual != EstadoJugador.Vivo && colisionesActivas == false)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Segura":
                print("segura");
                break;
            case "Combustible":
                llenarCombustible();
                break;
            case "Final":
                ProcesarFinal();
                break;
            case "Reparacion":
                audioSFX.PlayOneShot(sonidoCuracion);
                vida = 5;
                break;
            case "kamikaze":
                vida = 1;
                ProcesarGolpe();
                break;

            default:

                ProcesarGolpe();
                
                break;
        }
    }

    private void llenarCombustible()
    {
        combustible = 10;
        energia.SetActive(false);
        audioPropulsion.Stop();
        audioSFX.PlayOneShot(sonidoEnergia);
    }

    private void Propulsion()
    {
        if (combustible <= 0f && estadoActual != EstadoJugador.SinCombustible)
        {
            estadoActual = EstadoJugador.SinCombustible;

            audioSFX.Stop();
            audioPropulsion.Stop();
            partPropulsion.Stop();

            Invoke("Muerte", tiempoMuerte);
            return;
        }

        rbody.freezeRotation = true;
        
        rbody.AddRelativeForce(Vector3.up * Time.deltaTime * velocidadPropulsion);
        combustible -= consumoCombustibleSegundo * Time.deltaTime;

        if (!partPropulsion.isPlaying)
        {
            partPropulsion.Play();
        }

        if (!audioPropulsion.isPlaying)
        {
            audioPropulsion.PlayOneShot(sonidoPropulsion);
        }
    }

    private void ProcesarFinal()
    {
        audioSFX.Stop();
        audioPropulsion.Stop();
        audioSFX.PlayOneShot(sonidoVictoria);
        partGanar.Play();
        estadoActual = EstadoJugador.NivelCompleto;
        Invoke("PasarNivel", tiempoGanar);
    }

    private void PasarNivel()
    {
        int cantidadEscenas= SceneManager.sceneCountInBuildSettings;
        nivelActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = (nivelActual+1) % cantidadEscenas;

        if (siguienteEscena == cantidadEscenas)
        {
            siguienteEscena = 0;
        }

        SceneManager.LoadScene(siguienteEscena);
    } 

    private void ReiniciarNivel()
    {
        nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nivelActual);
    }

    private void ProcesarRotacion()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * velocidadRotacion * Time.deltaTime);
            /*var rotarDerecha = transform.rotation;
            rotarDerecha.z -= Time.deltaTime;
            transform.rotation = rotarDerecha;*/
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);
            /*var rotarDerecha = transform.rotation;
            rotarDerecha.z += Time.deltaTime;
            transform.rotation = rotarDerecha;*/
        }
    }


    private void procesarInput()
    {
        if (estadoActual == EstadoJugador.Vivo)
        {
            ProcesarPropulsion();
            ProcesarRotacion();
        }

        procesarInputDeDesarrollador();

    }

    private void procesarInputDeDesarrollador()
    {
        if (Input.GetKey(KeyCode.L)) 
        {
            PasarNivel();
        }
        else if (!Input.GetKey(KeyCode.B))
        {
            colisionesActivas = !colisionesActivas;
        }

    }

    private void ProcesarPropulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Propulsion();

        }
        else
        {
            audioPropulsion.Stop();
            partPropulsion.Stop();
        }
        rbody.freezeRotation = false;


    }
    private void ProcesarGolpe()
    {
        audioSFX.Stop();
        audioPropulsion.Stop(); 
        vida--;
        if (vida == 0)
        {
            Muerte();
        }
        else 
        {
            audioSFX.PlayOneShot(sonidoChoque);
        }
    }

    private void Muerte()
    {
        audioSFX.PlayOneShot(sonidoMuerte);
        partMuerte.Play();
        estadoActual = EstadoJugador.Muerto;
        partPropulsion.Stop();
        nave.SetActive(false);
        Invoke("ReiniciarNivel", tiempoMuerte);
    }
}
