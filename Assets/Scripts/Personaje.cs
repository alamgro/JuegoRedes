using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public float velocidadCaminando;
    public float velocidadSprint;
    public float velocidadGiro;
    public float fuerzaSalto;
    public Animator anim;
    public Transform posPies;
    [HideInInspector]
    public bool puedeMoverse;

    private float velocidad;
    private float velHorizontal, velVertical; //Variables para guardar el valor de GetAxis
    private bool estaEnPiso;
    private Rigidbody rb;
    private Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidad = velocidadCaminando;
        puedeMoverse = true; //Indica si el jugador puede moverse, rotar, o saltar.
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("VelY", rb.velocity.y); //Le hago saber a la animación cuál es la velocidad en Y del personaje
        vel = rb.velocity;
        CheckGround();

        if (puedeMoverse)
        {
            Movimiento();
            Rotacion();
            CheckSprint();
            Salto();
        }
        Rodar();
        Agacharse();
        Sentarse();

        rb.velocity = vel; //Actualizamos la velocidad del rigidbody
    }

    void Movimiento()
    {
        velHorizontal = Input.GetAxisRaw("Horizontal");
        velVertical = Input.GetAxisRaw("Vertical");
        Vector3 direccion = new Vector3(velHorizontal, 0f, velVertical).normalized;
        direccion = transform.worldToLocalMatrix.inverse * direccion; //Para que se mueva en la dirección que realmente está mirando el objeto

        anim.SetFloat("VelX", velHorizontal);
        anim.SetFloat("VelZ", velVertical);
        anim.SetBool("Moviendose", false);

        if (direccion != Vector3.zero) //Si hay alguna entrada de movimiento, entonces aviso a mis animaciones que estoy moviéndome
        {
            anim.SetBool("Moviendose", true);
        }

        rb.MovePosition(transform.position + (direccion * velocidad) * Time.deltaTime); //mueve al jugador
    }

    void Rotacion() //Rotar sobre sí mismo
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * velocidadGiro * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);
        }
    }

    void Salto() //Le da la capacidad de dobre salto
    {
        vel.y = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && estaEnPiso) //Checa si presiono espacio y si le quedan brincos disponibles
        {
            //print("Salto");
            anim.Play("Saltar Start");
            vel.y = fuerzaSalto;
        }
    }

    void CheckSprint() //Revisa cuándo debe de correr
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Corriendo", true);
            anim.SetFloat("VelX", velHorizontal * 2);
            anim.SetFloat("VelZ", velVertical * 2);
            velocidad = velocidadSprint;
        }
        else
        {
            anim.SetBool("Corriendo", false);
            velocidad = velocidadCaminando;
        }
    }

    void Agacharse() //Activa la animación de agachado
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Agachado", true);
        }
        else
        {
            anim.SetBool("Agachado", false);
        }
    }

    void Sentarse() //Activa la animación de sentado
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            anim.SetBool("Sentado", true);
        }
        else
        {
            anim.SetBool("Sentado", false);
        }
    }

    void Rodar() //Activa animación de rodar
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetTrigger("Rodar");
        }
    }

    void CheckGround() //Verifica si el jugador está en el piso
    {
        if(Physics.Raycast(posPies.position, Vector3.down, 0.1f))
        {
            estaEnPiso = true;
        }
        else
        {
            estaEnPiso = false;
        }
        anim.SetBool("PisandoSuelo", estaEnPiso);  //Avisa si estoy en el suelo o no
    }

}
