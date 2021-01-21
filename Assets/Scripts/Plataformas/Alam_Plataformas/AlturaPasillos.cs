using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlturaPasillos : MonoBehaviour
{
    /*
     * Este script cambia la altura inicial del objeto que lo contenga. 
     * El objeto aparece en una altura aleatoria dentro de un rango.
     */ 

    private float alturaMin, alturaMax;

    void Start()
    {
        alturaMin = -0.6f;
        alturaMax = 0.6f;
        float alturaRandom = Random.Range(alturaMin, alturaMax);
        transform.position = transform.position + (Vector3.up * alturaRandom);
    }
}
