using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class ControlDeNave : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;

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
    [SerializeField] public float combustible = 200.0f;
    [SerializeField] public float consumoCombustibleSegundo = 10.0f;

    [SerializeField] private float velocidadPropulsion = 400.0f;
    [SerializeField] private float velocidadRotacion = 100.0f;

    [SerializeField] private float tiempoMuerte = 3.0f;
    [SerializeField] private float tiempoGanar = 2.0f;

    [SerializeField] AudioClip sonidoPropulsion;
    [SerializeField] AudioClip sonidoVictoria;
    [SerializeField] AudioClip sonidoMuerte;
    [SerializeField] AudioClip sonidoChoque;

    [SerializeField] ParticleSystem partMuerte;
    [SerializeField] ParticleSystem partGanar;
    [SerializeField] ParticleSystem partPropulsion;
    // Start is called before the first frame update



    void Start()
    {
        DynamicGI.UpdateEnvironment();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime + "seg. " + (1.0f / Time.deltaTime) + "FPS"); 
        procesarInput();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (estadoActual != EstadoJugador.Vivo)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Segura":
                print("segura");
                break;
            case "Combustible":
                print("Se ha tomado Combustible");
                //implementar combustible
                break;
            case "Final":
                ProcesarFinal();
                break;
            case "Reparacion":
                vida++;
                break;
            default:

                ProcesarGolpe();
                
                break;
        }
    }

    private void Propulsion()
    {
        if (combustible <= 0f)
        {
            estadoActual = EstadoJugador.SinCombustible;
            audioSource.Stop();
            partPropulsion.Stop();
            return;
        }

        rigidbody.freezeRotation = true;
        
        rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * velocidadPropulsion);
        combustible -= consumoCombustibleSegundo * Time.deltaTime;

        if (!partPropulsion.isPlaying)
        {
            partPropulsion.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sonidoPropulsion);
        }
    }

    private void PasarNivel()
    {

        nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nivelActual + 1);

    } 

    private void Muerte()
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
    private void ProcesarFinal()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sonidoVictoria);
        partGanar.Play();
        estadoActual = EstadoJugador.NivelCompleto;
        Invoke("PasarNivel", tiempoGanar);
    }

    private void procesarInput()
    {
        if (estadoActual == EstadoJugador.Vivo)
        {
            ProcesarPropulsion();
            ProcesarRotacion();
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
            audioSource.Stop();
            partPropulsion.Stop();
        }
        rigidbody.freezeRotation = false;


    }
    private void ProcesarGolpe()
    {
        audioSource.Stop();
        vida--;
        if (vida == 0)
        {
            audioSource.PlayOneShot(sonidoMuerte);
            partMuerte.Play();
            estadoActual = EstadoJugador.Muerto;
            Invoke("Muerte", tiempoMuerte);
        } 
        else 
        {
            audioSource.PlayOneShot(sonidoChoque);
        }
    }
}
