using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarPaso : MonoBehaviour
{
    public GameObject[] plataformas;
    public GameObject plataformasFinal;

    public float espacio; //espacio requerido por plataforma siguente solo en Z
    public float ejeZ; //Pos de inicio

    public Vector3 inicio; //La primera plataforma

    public int numPlataformas; //Piezas totales del cuerpo


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numPlataformas; i++)
        {

            int randPlat = Random.Range(0, plataformas.Length); //Random del tamano del array

            Instantiate(plataformas[randPlat], new Vector3(0f, 0f, ejeZ), Quaternion.identity); //Lo instanciamos

            ejeZ += espacio; //cambiamos la pos en Z 


        }
        Instantiate(plataformasFinal, new Vector3(0f, 0f, ejeZ), Quaternion.identity); //Lo instanciamoss

     


    }

    // Update is called once per frame
    void Update()
    {      
       

    }
}
