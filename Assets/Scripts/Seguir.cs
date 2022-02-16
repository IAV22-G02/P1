/*
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

        protected Vector3 objectivePos;

        public override void Start()
        {
            objetivo = SensorialManager.instance.getTarget();
            objectivePos = objetivo.transform.position;
        }

        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        public override Direccion GetDirection()
        {
            Direccion direccion = new Direccion();

            //GET MAIN DIRECTION
            direccion.lineal = objectivePos - transform.position;
            direccion.lineal.Normalize();

            direccion.orientation = Vector3.SignedAngle(Vector3.forward, new Vector3(direccion.lineal.x, 0.0f, direccion.lineal.z), Vector3.up);

            direccion.lineal *= agente.aceleracionMax;

            // Podríamos meter una rotación automática en la dirección del movimiento, si quisiéramos
            return direccion;
        }
    }
}
