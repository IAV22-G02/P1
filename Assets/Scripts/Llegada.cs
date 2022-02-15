using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Llegada : ComportamientoAgente
    {
        List<GameObject> targets;
        Rigidbody rb;

        float maxAcceleration;
        float maxVelocity;

        //Radius for arriving at the target
        float targetRadius;

        //Radius for beginning to slow down
        float slowRadius;

        //Time over which to achieve target speed
        float timeToTarget = 0.1;

        public override void Start()
        {
            rb = GetComponent<Rigidbody>();
            targets = SensorialManager.instance.getRats();
        }

        /// <summary>
        /// Obtiene la dirección
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

            //AVOID COLLISION
            float shortestTime = Mathf.Infinity;

            GameObject firstTarget = null;
            float firstMinSeparation = 0;
            float firstDistance = 0;
            Vector3 firstRelativePos = Vector3.zero;
            Vector3 firstRelativeVel = Vector3.zero;
            Vector3 relativePos;


            foreach (GameObject target in targets)
            {

                if (target == this.gameObject)
                    continue;

                Rigidbody targetRb = target.GetComponent<Rigidbody>();

                relativePos = target.transform.position - this.gameObject.transform.position;
                Vector3 relativeVel = targetRb.velocity - rb.velocity;

                float relativeSpeed = relativeVel.magnitude;

                float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

                float distance = relativePos.magnitude;

                float minSeparation = distance - relativeSpeed * timeToCollision;
                if (minSeparation > radiusFactorSeparation * radius)
                    continue;

                if (timeToCollision > 0 && timeToCollision < shortestTime)
                {
                    shortestTime = timeToCollision;
                    firstTarget = target;
                    firstMinSeparation = minSeparation;
                    firstDistance = distance;
                    firstRelativePos = relativePos;
                    firstRelativeVel = relativeVel;
                }
            }

            if (firstTarget == null)
            {
                direccion.lineal *= agente.aceleracionMax;
                return direccion;
            }

            if (firstMinSeparation <= 0 || firstDistance < radiusFactorSeparation * radius)
                relativePos = firstTarget.transform.position - gameObject.transform.position;
            else relativePos = firstRelativePos + firstRelativeVel * shortestTime;

            relativePos.Normalize();

            direccion.lineal += relativePos * agente.aceleracionMax;

            // Podríamos meter una rotación automática en la dirección del movimiento, si quisiéramos
            return direccion;
        }
    }
}