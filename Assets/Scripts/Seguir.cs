﻿/*
   Copyright (C) 2020 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/


namespace UCM.IAV.Movimiento
{
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Seguir : ComportamientoAgente
    {

        [SerializeField]
        float threshold;

        [SerializeField]
        float decayCoefficient;

        List<GameObject> targets;
        Rigidbody rb;

        SphereCollider sphColl;

        float radiusFactorSeparation = 4;

        float radius;

        public override void Start()
        {
            sphColl = GetComponent<SphereCollider>();
            radius = sphColl.radius;
            objetivo = SensorialManager.instance.getTarget();
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

            direccion.lineal += Separate();

            direccion.lineal *= agente.aceleracionMax;

            // Podríamos meter una rotación automática en la dirección del movimiento, si quisiéramos
            return direccion;
        }

        public Vector3 Separate()
        {
            Vector3 direccion = new Vector3();

            float minDistance = -1f;
            float strength = 100;

            // Para cada entidad
            foreach (GameObject rat in targets)
            {
                // Comprobar que t está cerca
                Vector3 dirOpossite = transform.position - rat.transform.position;

                float distance = dirOpossite.magnitude;

                // Si entra en el área
                if (distance < threshold)
                {
                    // Añadir aceleración
                    dirOpossite.Normalize();
                    direccion += dirOpossite;

                    // Cogemos la fuerza en base al target más cercano
                    if (distance < minDistance || minDistance == -1)
                    {
                        // Fuerza de repulsión
                        strength = Mathf.Min(decayCoefficient / (distance * distance), agente.aceleracionMax);
                        minDistance = distance;
                    }
                }
            }

            // Aplicamos la dirección y fuerza al vector
            direccion.Normalize();
            direccion *= strength;

            return direccion;
        }

        public Direccion AvoidCollision(ref Direccion direccion)
        {
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
                if (target == gameObject) continue;

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
                //    return direccion;
                //}

                if (firstMinSeparation <= 0 || firstDistance < radiusFactorSeparation * radius)
                    relativePos = gameObject.transform.position - firstTarget.transform.position;
                else relativePos = firstRelativePos + firstRelativeVel * shortestTime;

                //relativePos.Normalize();

                direccion.lineal = relativePos;
            }
            return direccion;
        }
    }
}
