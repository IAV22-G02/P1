/*    
   Copyright (C) 2020 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).

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

        AudioSource audio;
        public override void Start(){
            audio = GetComponent<AudioSource>();
            SensorialManager.instance.setTarget(this.gameObject);   
            SensorialManager.instance.getRats().Add(this.gameObject);   
        }
        public override void Update()
        {
            base.Update();

            if (Input.GetKey(KeyCode.Space))
            {
                if (!audio.isPlaying)
                    audio.Play();
                else audio.Pause();

                SensorialManager.instance.PlayFlauta(true);
            }
            else
            {
                SensorialManager.instance.PlayFlauta(false);
            }
        }
        /// <summary>
        /// Obtiene la direcci�n
        /// </summary>
        /// <returns></returns>
        public override Direccion GetDirection()
        {
            Direccion direccion = new Direccion();
            direccion.lineal.x = Input.GetAxis("Horizontal");
            direccion.lineal.z = Input.GetAxis("Vertical");
            direccion.lineal.Normalize();
            direccion.lineal *= agente.aceleracionMax;

            // Podr�}mos meter una rotaci�n autom�tica en la direcci�n del movimiento, si quisi�ramos

            return direccion;
        }
    }
}