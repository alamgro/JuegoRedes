using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecargaDeEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); //Recargamos la escena de testeo para crear otro mapa
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //Cerramos desde Unity
        }

        float poscionX = Input.GetAxis("Mouse X");
        float poscionY = Input.GetAxis("Mouse Y");

        //gameObject.transform.rotation = Quaternion.Euler(poscionX, poscionY, 0);
        transform.Rotate(Vector3.up * poscionY);
        transform.Rotate(Vector3.down * poscionX);



    }
}
