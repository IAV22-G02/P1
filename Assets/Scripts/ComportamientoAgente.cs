/*    
   Copyright (C) 2020 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform·tica de la Universidad Complutense de Madrid (EspaÒa).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
namespace UCM.IAV.Movimiento
{

    using UnityEngine;

    /// <summary>
    /// Clase abstracta que funciona como plantilla para todos los comportamientos de agente
    /// </summary>
    public class ComportamientoAgente : MonoBehaviour
    {
        /// <summary>
        /// Peso
        /// </summary>
        public float peso = 1.0f;
        /// <summary>
        /// Prioridad
        /// </summary>
        public int prioridad = 1;
        /// <summary>
        /// Objetivo (para aplicar o representar el comportamiento, depende del comportamiento que sea)
        /// </summary>
        public GameObject objetivo;
        /// <summary>
        /// Agente que hace uso del comportamiento
        /// </summary>
        protected Agente agente;

        /// <summary>
        /// Al despertar, establecer el agente que harÅEuso del comportamiento
        /// </summary>
        public virtual void Awake()
        {
            agente = gameObject.GetComponent<Agente>();

            objetivo = GameObject.FindGameObjectWithTag("Player");
        }

        public virtual void Start() {

        }
        /// <summary>
        /// En cada tick, establecer la direcciÛn que corresponde al agente, con peso o prioridad si se est·n usando
        /// </summary>
        public virtual void Update()
        {
            if (agente == null) return;

            if (agente.combinarPorPeso)
                agente.SetDirection(GetDirection(), peso);
            else if (agente.combinarPorPrioridad)
                agente.SetDirection(GetDirection(), prioridad);
            else
                agente.SetDirection(GetDirection());
        }

        /// <summary>
        /// Devuelve la direccion calculada
        /// </summary>
        /// <returns></returns>
        public virtual Direccion GetDirection()
        {
            return new Direccion();
        }

        /// <summary>
        /// Asocia la rotaciÛn al rango de 360 grados
        /// </summary>
        /// <param name="rotacion"></param>
        /// <returns></returns>
        public float RadianesAGrados(float rotacion)
        {
            rotacion %= 360.0f;
            if (Mathf.Abs(rotacion) > 180.0f)
            {
                if (rotacion < 0.0f)
                    rotacion += 360.0f;
                else
                    rotacion -= 360.0f;
            }
            return rotacion;
        }

        /// <summary>
        /// Cambia el valor real de la orientaciÛn a un Vector3 
        /// </summary>
        /// <param name="orientacion"></param>
        /// <returns></returns>
        public Vector3 OriToVec(float orientacion)
        {
            Vector3 vector = Vector3.zero;
            vector.x = Mathf.Sin(orientacion * Mathf.Deg2Rad) * 1.0f;
            vector.z = Mathf.Cos(orientacion * Mathf.Deg2Rad) * 1.0f;
            return vector.normalized;
        }
    }
}
