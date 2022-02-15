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

    using UnityEngine;

    /// <summary>
    /// Clara para el comportamiento de agente que consiste en ser el jugador
    /// </summary>
    public class ControlJugador: ComportamientoAgente
    {
        bool isPlaying = false;


        public override void Start()
        {
            SensorialManager.instance.setTarget(this.gameObject);   
        }
        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Space)) {
                isPlaying = !isPlaying;
                SensorialManager.instance.PlayFlauta(isPlaying);//Tocar esto para que sea un toggle
            }
        }
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        public override Direccion GetDirection()
        {
            Direccion direccion = new Direccion();
            direccion.lineal.x = Input.GetAxis("Horizontal");
            direccion.lineal.z = Input.GetAxis("Vertical");
            direccion.lineal.Normalize();
            direccion.lineal *= agente.aceleracionMax;

            // Podrú}mos meter una rotación automática en la dirección del movimiento, si quisiéramos

            return direccion;
        }
    }
}