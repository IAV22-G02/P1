using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class EvasionObstaculos : ComportamientoAgente
    {
        Ray rayC, rayR, rayL;
        //Capa de Colision
        public LayerMask layer;
        //Distancia minima a un muro(debe ser mayor al radio del gameobject
        [SerializeField]
        float avoidDistance = 1.0f;

        //Distancia de vision del raycast
        public float lookAhead = 5.0f;
        public float lookSide = 2.0f;

    //    public override Direccion GetDirection()
    //    {
    //        Direccion direccion = new Direccion();
    //        //Calculo de colision del rayo
    //        rayC = new Ray(transform.position, transform.forward);
    //        rayR = new Ray(transform.position, transform.forward * 2 + transform.right);
    //        rayL = new Ray(transform.position, transform.forward * 2 - transform.right);
    //        RaycastHit hitC, hitR, hitL;


    //        if (Physics.Raycast(rayR, out hitR, lookSide, layer))
    //        {
    //            // Find the line from the gun to the point that was clicked.
    //            Vector3 incomingVec = hitR.point - transform.position;
    //            // Use the point's normal to calculate the reflection vector.
    //            Vector3 reflectVec = Vector3.Reflect(incomingVec, hitR.normal);

    //            // Draw lines to show the incoming "beam" and the reflection.
    //            Debug.DrawLine(transform.position, hitR.point, Color.red);
    //            Debug.DrawRay(hitR.point, reflectVec, Color.green);

    //        }
    //        else if (Physics.Raycast(rayC, out hitC, lookAhead, layer))
    //        {
    //            // Find the line from the gun to the point that was clicked.
    //            Vector3 incomingVec = hitC.point - transform.position;
    //            // Use the point's normal to calculate the reflection vector.
    //            Vector3 reflectVec = Vector3.Reflect(incomingVec, hitC.normal);

    //            // Draw lines to show the incoming "beam" and the reflection.
    //            Debug.DrawLine(transform.position, hitC.point, Color.red);
    //            Debug.DrawRay(hitC.point, reflectVec, Color.green);
    //        }
    //        else if (Physics.Raycast(rayL, out hitL, lookSide, layer))
    //        {
    //            // Find the line from the gun to the point that was clicked.
    //            Vector3 incomingVec = hitL.point - transform.position;
    //            // Use the point's normal to calculate the reflection vector.
    //            Vector3 reflectVec = Vector3.Reflect(incomingVec, hitL.normal);

    //            // Draw lines to show the incoming "beam" and the reflection.
    //            Debug.DrawLine(transform.position, hitL.point, Color.red);
    //            Debug.DrawRay(hitL.point, reflectVec, Color.green);
    //        }
    //        else
    //        {
    //            if (this.gameObject.GetComponent<dogMovement>() != null)
    //                return this.gameObject.GetComponent<dogMovement>().GetDirection();

    //            else if(this.gameObject.GetComponent<Seguir>() != null && this.gameObject.GetComponent<Seguir>().enabled)
    //                return this.gameObject.GetComponent<Seguir>().GetDirection();

    //            else 
    //                return null;
    //        }

    //        direccion.lineal *= agente.aceleracionMax;
    //        return direccion;
    //    }

    }
}
