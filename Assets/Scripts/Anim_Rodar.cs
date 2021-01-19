using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Rodar : StateMachineBehaviour
{
    //Habrá un collider diferente dependiendo si está de pie o está agachado

    private Personaje personaje;
    private Rigidbody rb;
    private Vector3 vel;
    private CapsuleCollider colliderDePie;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (personaje == null)
        {
            personaje = GameObject.FindGameObjectWithTag("Player").GetComponent<Personaje>();
            colliderDePie = personaje.gameObject.GetComponent<CapsuleCollider>();
        }
        rb = personaje.GetComponent<Rigidbody>();
        vel = rb.velocity;
        personaje.puedeMoverse = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Empieza a dar la velocidad de movimiento a partir del 25% de la animación y termina en el 80%, para que no se vea tan raro
        if (stateInfo.normalizedTime >= 0.25f && stateInfo.normalizedTime <= 0.9f) 
        {
            if (rb.velocity.y > -1f) //solo puede dar velocidad hacia adelante si no está cayendo
            {
                //INTERCAMBIAR COLLIDER - "DE PIE" A "AGACHADO"
                colliderDePie.isTrigger = true;

                vel = Vector3.forward * 5f; //Velocidad hacia adelante
                vel = personaje.transform.worldToLocalMatrix.inverse * vel; //Se asegura que el vector sea hacia en frente con respecto al jugador
            }
        }
        else
        {
            //INTERCAMBIAR COLLIDER - "AGACHADO" A "DE PIE"
            colliderDePie.isTrigger = false;

            vel = Vector3.forward * 0f; //Mientras esté preparando la vuelta, todavía no se debe de mover de su lugar
        }

        vel.y = rb.velocity.y;
        rb.velocity = vel;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        personaje.puedeMoverse = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
