using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlDeNave : MonoBehaviour
{
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audioSource;

    enum EstadoNave { Viva, Muerta, NivelCompleto};
    private EstadoNave estadoNave = EstadoNave.Viva;
    private MainMenu m = new MainMenu();
    private static int vidas = 2;

    [SerializeField] float velocidadPropulsion = 200.0f;  //valor standar ya que delta es aprox 0.2
    [SerializeField] float velocidadRotacion = 200.0f;
    [SerializeField] float  timerMuerte = 1.0f;
    [SerializeField] float  timerNivelCompleto = 1.0f;
    
    [SerializeField] AudioClip sonidoPropulsion;
    [SerializeField] AudioClip sonidoNivelCompleto;
    [SerializeField] AudioClip sonidoMuerte;

    //[SerializeField] ParticleSystem partMuerte;
    //[SerializeField] ParticleSystem partNivelCompleto;
    [SerializeField] ParticleSystem partPropulsion;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>(); // no es necesario
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarInput();
    }

     private void OnCollisionEnter(Collision collision)
    {
        if(estadoNave != EstadoNave.Viva)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "ColisionSegura":
                estadoNave = EstadoNave.Viva;
                //print("Colision Segura ...");
                break;
            case "Combustible":
                print("Combustible");
                break;
            case "Aterrizaje":
                ProcesarNivelCompleto();
                Invoke("PasarNivel0", timerNivelCompleto);
                print("Llegaste");
                break;
            case "ColisionPeligrosa":
                ProcesarMuerte();
                Invoke("Muerte0", timerMuerte);
                break;
            case "Aterrizaje1":
                ProcesarNivelCompleto();
                Invoke("PasarNivel1", timerNivelCompleto);
                print("Llegaste");
                break;
            case "ColisionPeligrosa1":
                ProcesarMuerte();
                if(vidas <= 0){
                    vidas = 2;
                    m.CargarMenuReintentar();
                }else{
                    Invoke("Muerte1", timerMuerte);
                    vidas = vidas - 1;
                    print(vidas);
                }
                break;
            case "Aterrizaje2":
                ProcesarNivelCompleto();
                Invoke("PasarNivel2", timerNivelCompleto);
                print("Llegaste");
                break;
            case "ColisionPeligrosa2":
                ProcesarMuerte();
                Invoke("Muerte2", timerMuerte);
                break;
            case "Aterrizaje3":
                ProcesarNivelCompleto();
                Invoke("PasarNivel3", timerNivelCompleto);
                print("Llegaste");
                break;
            case "ColisionPeligrosa3":
                ProcesarMuerte();
                Invoke("Muerte3", timerMuerte);
                break;
            case "Aterrizaje4":
                ProcesarNivelCompleto();
                Invoke("PasarNivel4", timerNivelCompleto);
                print("Llegaste");
                break;
            case "ColisionPeligrosa4":
                ProcesarMuerte();
                Invoke("Muerte4", timerMuerte);
                break;
            case "Aterrizaje5":
                ProcesarNivelCompleto();
                Invoke("PasarNivel5", timerNivelCompleto);
                print("Llegaste");
                break;
            case "ColisionPeligrosa5":
                ProcesarMuerte();
                Invoke("Muerte5", timerMuerte);
                break;
            
        }

    }

    private void ProcesarNivelCompleto()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sonidoNivelCompleto);
        //partNivelCompleto.Play();
        estadoNave = EstadoNave.NivelCompleto;
        m.CargarMenuSiguiente();
    }

    private void ProcesarMuerte()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sonidoMuerte);
        //partMuerte.Play();
        estadoNave = EstadoNave.Muerta;
    }
////////////////////////////////////////////////////////

    private void Muerte0()
    {
        SceneManager.LoadScene("NivelDemo");
    }

    private void PasarNivel0()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Muerte1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    private void PasarNivel1()
    {
        SceneManager.LoadScene("MenuNextLvl");
    }
    private void Muerte2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    private void PasarNivel2()
    {
        SceneManager.LoadScene("Nivel3");
    }

    private void Muerte3()
    {
        SceneManager.LoadScene("Nivel3");
    }

    private void PasarNivel3()
    {
        SceneManager.LoadScene("Nivel4");
    }

    private void Muerte4()
    {
        SceneManager.LoadScene("Nivel4");
    }

    private void PasarNivel4()
    {
        SceneManager.LoadScene("Nivel5");
    }

    private void Muerte5()
    {
        SceneManager.LoadScene("Nivel5");
    }

    private void PasarNivel5()
    {
        SceneManager.LoadScene("Menu");
    }

////////////////////////////////////////////////
    private void ProcesarInput()
    {
        if(estadoNave == EstadoNave.Viva)
        {
            ProcesarPropulsion();
            ProcesarRotacion();
        }
    }

    private void ProcesarPropulsion()
    {
        if(Input.GetKey(KeyCode.Space))
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

    private void Propulsion()
    {
        rigidbody.freezeRotation = true;
        rigidbody.AddRelativeForce(Vector3.up * velocidadPropulsion * Time.deltaTime);
        if(!partPropulsion.isPlaying)
        {
            partPropulsion.Play();
        }
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sonidoPropulsion);
        }
        //print("Propulsor");
    }

    private void ProcesarRotacion()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * velocidadRotacion * Time.deltaTime); //-Vector3.forward
            //print("Rotar Derecha");
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);
            //print("Rotar Izquierda");
        }
    }
}
