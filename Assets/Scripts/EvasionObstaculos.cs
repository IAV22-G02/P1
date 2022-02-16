using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class EvasionObstaculos : ComportamientoAgente
    {
        Ray ray1, ray2, ray3;
        //Capa de Colision
        public LayerMask layer;
        //Distancia minima a un muro(debe ser mayor al radio del gameobject
        [SerializeField]
        float avoidDistance = 1.0f;

        //Distancia de vision del raycast
        public float lookAhead = 5.0f;
        public float lookSide = 2.0f;


        public override void Start()
        {
            base.Start();
        }
        // Start is called before the first frame update
        public override Direccion GetDirection()
        {
            Direccion direccion = base.GetDirection();
            //Calculo de colision del rayo
            ray1 = new Ray(transform.position, transform.forward);
            ray2 = new Ray(transform.position, transform.forward*2 + transform.right);
            ray3 = new Ray(transform.position, transform.forward*2 - transform.right);
            RaycastHit hit1, hit2, hit3;


            bool isHit = false;
            if (Physics.Raycast(ray1, out hit1, lookAhead, layer))
            {
                // Find the line from the gun to the point that was clicked.
                Vector3 incomingVec = hit1.point - transform.position;
                // Use the point's normal to calculate the reflection vector.
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit1.normal);

                // Draw lines to show the incoming "beam" and the reflection.
                Debug.DrawLine(transform.position, hit1.point, Color.red);
                Debug.DrawRay(hit1.point, reflectVec, Color.green);

                direccion.lineal += (transform.position - hit1.normal) * avoidDistance;

                isHit = true;
            }
            if (Physics.Raycast(ray2, out hit2, lookSide, layer))
            {
                // Find the line from the gun to the point that was clicked.
                Vector3 incomingVec = hit2.point - transform.position;
                // Use the point's normal to calculate the reflection vector.
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit2.normal);

                // Draw lines to show the incoming "beam" and the reflection.
                Debug.DrawLine(transform.position, hit2.point, Color.red);
                Debug.DrawRay(hit2.point, reflectVec, Color.green);

                direccion.lineal += (transform.position - hit1.normal) * avoidDistance;

                isHit = true;
            }
            if (Physics.Raycast(ray3, out hit3, lookSide, layer))
            {
                // Find the line from the gun to the point that was clicked.
                Vector3 incomingVec = hit3.point - transform.position;
                // Use the point's normal to calculate the reflection vector.
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit3.normal);

                // Draw lines to show the incoming "beam" and the reflection.
                Debug.DrawLine(transform.position, hit3.point, Color.red);
                Debug.DrawRay(hit3.point, reflectVec, Color.green);

                direccion.lineal += (transform.position - hit1.normal) * avoidDistance;

                isHit = true;
            }

            return direccion;
        }
    }
}
