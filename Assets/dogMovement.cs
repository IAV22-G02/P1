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

        List<GameObject> targets;
        Rigidbody rb;

        SphereCollider sphColl;

        float radius;

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
            direccion.lineal = objetivo.transform.position - transform.position;
            direccion.lineal.Normalize();

            direccion.orientation = Vector3.SignedAngle(Vector3.forward, new Vector3(direccion.lineal.x, 0.0f, direccion.lineal.z), Vector3.up);

            if(targets.Count > 0){
                direccion.lineal = Separate();
            }

            direccion.lineal *= agente.aceleracionMax;

            // Podr�amos meter una rotaci�n autom�tica en la direcci�n del movimiento, si quisi�ramos
            return direccion;
        }

        public Vector3 Separate()
        {
            Vector3 direccion = new Vector3();

            float minDistance = -1f;
            float strength = 1;

            // Para cada entidad
            foreach (GameObject rat in targets)
            {
                // Comprobar que t est� cerca
                Vector3 dirOpossite = transform.position - rat.transform.position;

                float distance = dirOpossite.magnitude;

                // Si entra en el �rea
                if (distance < threshold)
                {
                    // A�adir aceleraci�n
                    dirOpossite.Normalize();
                    direccion += dirOpossite;

                    // Cogemos la fuerza en base al target m�s cercano
                    if (distance < minDistance || minDistance == -1)
                    {
                        // Fuerza de repulsi�n
                        strength = Mathf.Min(decayCoefficient / (distance * distance), agente.aceleracionMax);
                        minDistance = distance;
                    }
                }
            }

            // Aplicamos la direcci�n y fuerza al vector
            direccion.Normalize();
            direccion *= strength;

            return direccion;
        }
    }
}