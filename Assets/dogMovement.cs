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

        #region WallAvoidance 
        //Capa de Colision
        public LayerMask layer;
        //Distancia minima a un muro(debe ser mayor al radio del gameobject
        [SerializeField]
        float avoidDistance;

        //Distancia de vision del raycast
        public float lookAhead = 12.0f;
        public float lookSide = 8.0f;
        #endregion


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
        public override Direccion GetDirection(){
            Direccion direccion = new Direccion();
            if (!enabled) return direccion;

            //GET MAIN DIRECTION
            float acceleration = agente.aceleracionMax;

            direccion.lineal = objetivo.transform.position - transform.position;

            direccion.orientation = Vector3.SignedAngle(Vector3.forward, new Vector3(direccion.lineal.x, 0.0f, direccion.lineal.z), Vector3.up);
            direccion.lineal += WallAvoidance();
            direccion.lineal.Normalize();

            direccion.lineal += Arrive(ref acceleration);
            if (targets.Count > 0){
                direccion.lineal = Separate();
            }

            direccion.lineal.Normalize();
            direccion.lineal *= acceleration;
            direccion.angular = 0;

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

        public Vector3 WallAvoidance() {
            Vector3 direccion = new Vector3();
            //FORWARD
            Vector3 directionRay = transform.forward;
            checkHitRayCast(ref direccion, directionRay, lookAhead);
            //RIGHT
            directionRay = transform.forward + (transform.right);
            checkHitRayCast(ref direccion, directionRay, lookSide);
            //LEFT
            directionRay = transform.forward + (transform.right * -1);
            checkHitRayCast(ref direccion, directionRay, lookSide);

            return direccion;
        }

        private void checkHitRayCast(ref Vector3 directionAcc, Vector3 directionRay, float size){
            Vector3 from = transform.position;
            from.y = from.y + 0.5f;
            Debug.DrawRay(from , directionRay * size, Color.green);
            RaycastHit hit1;
            if (Physics.Raycast(from, directionRay, out hit1, size)) {
                if(hit1.collider.gameObject.GetComponent<BoxCollider>() != null) {
                    Vector3 dir = hit1.point + hit1.normal * avoidDistance;
                    Debug.DrawRay(hit1.point, dir, Color.red);
                    directionAcc += dir;
                }
            }
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