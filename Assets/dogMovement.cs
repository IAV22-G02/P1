using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class dogMovement : ComportamientoAgente {
        [SerializeField]
        float threshold;

        [SerializeField]
        float decayCoefficient;
            
        [SerializeField]
        float slowRadius;

        float radius;

        List<GameObject> targets;
        Rigidbody rb;

        SphereCollider sphColl;


        float timeToTarget = 0.01f;

        public override void Awake(){
            base.Awake();
            targets = new List<GameObject>();
        }

        public override void Start() {
            sphColl = GetComponent<SphereCollider>();
            radius = sphColl.radius;

            rb = GetComponent<Rigidbody>();
        }

        public ref List<GameObject> getRats(){
            return ref targets;
        }

        /// <summary>
        /// Obtiene la direcci�n
        /// </summary>
        /// <returns></returns>
        public override Direccion GetDirection()
        {

            Direccion direccion = new Direccion();
            if (!this.enabled) return direccion;

            //GET MAIN DIRECTION
            float acceleration = agente.aceleracionMax;

            direccion.lineal = objetivo.transform.position - transform.position;
            direccion.lineal.Normalize();

            direccion.orientation = Vector3.SignedAngle(Vector3.forward, new Vector3(direccion.lineal.x, 0.0f, direccion.lineal.z), Vector3.up);

            direccion.lineal += Arrive(ref acceleration);
            if (targets.Count > 0){
                direccion.lineal = Separate();
            }

            direccion.lineal *= acceleration;

            // Podr�}mos meter una rotaci�n autom�tica en la direcci�n del movimiento, si quisi�ramos
            return direccion;
        }

        public Vector3 Arrive(ref float acceleration){
            Vector3 dir = transform.position - objetivo.transform.position;
            float distance = dir.magnitude;

            if (distance > slowRadius)
                dir = Vector3.zero;
            else acceleration = 0;

            return dir;
        }

        public Vector3 Separate()
        {
            Vector3 direccion = new Vector3();

            float minDistance = -1f;
            float strength = 1;

            // Para cada entidad
            foreach (GameObject rat in targets)
            {
                Vector3 dirOpossite = transform.position - rat.transform.position;

                float distance = dirOpossite.magnitude;

                if (distance < threshold)
                {
                    dirOpossite.Normalize();
                    direccion += dirOpossite;

                    if (distance < minDistance || minDistance == -1)
                    {
                        strength = Mathf.Min(decayCoefficient / (distance * distance), agente.aceleracionMax);
                        minDistance = distance;
                    }
                }
            }

            direccion.Normalize();
            direccion *= strength;

            return direccion;
        }
    }
}