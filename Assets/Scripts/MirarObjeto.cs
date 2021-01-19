using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarObjeto : MonoBehaviour
{
    public Animator anim;
    private Transform objQueObserva;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (objQueObserva != null)
        {
            anim.SetLookAtWeight(1f);
            anim.SetLookAtPosition(objQueObserva.position);
        }
        else
        {
            anim.SetLookAtWeight(0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Cambia el objeto que debe de observar
        if(other.gameObject.CompareTag("Cilindro"))
        {
            objQueObserva = other.transform; //el objeto cercano será el objeto que el Player mirará
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Cuando se aleja lo suficiente del personaje, deja de ver a ese personaje
        if (other.gameObject.CompareTag("Cilindro"))
        {
            objQueObserva = null;
        }
    }
}
