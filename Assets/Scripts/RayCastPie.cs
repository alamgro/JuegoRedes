using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPie : MonoBehaviour
{
    public Animator anim;
    public LayerMask mask;
    //private bool puedeMoverPie = false;
    [Range (0f, 1f)]
    public float distanciaSuelo;

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            //Obtener el control de las rotaciones y posiciones de los pies
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            //Ray cast para el pie DERECHO
            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down); //Rayo a partir de la posición del pie, hacia abajo
            if(Physics.Raycast(ray, out hit, distanciaSuelo + 1, mask))
            {
                //Aquí ya pegó con algo, en este caso se revisa que después sea piso lo que toca
                if (hit.transform.CompareTag("Piso"))
                {
                    Vector3 posicionPie = hit.point;
                    posicionPie.y += distanciaSuelo;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, posicionPie);
                    //Acomoda el pie para que la rotación coincida con el del plano en el que está parado
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation); 
                }
            }

            //Ray cast para el pie IZQUIERDO
            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanciaSuelo + 1, mask))
            {
                //Aquí ya pegó con algo, en este caso se revisa que después sea piso lo que toca
                if (hit.transform.CompareTag("Piso"))
                {
                    Vector3 posicionPie = hit.point;
                    posicionPie.y += distanciaSuelo;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, posicionPie);
                    //Acomoda el pie para que la rotación coincida con el del plano en el que está parado
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
                }
            }

        }
    }


}
