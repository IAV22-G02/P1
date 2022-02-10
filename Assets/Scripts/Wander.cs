using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Wander : ComportamientoAgente
    {
        //If you change one you have to change the other uWu
        [SerializeField]
        float timeToChange = 1.2f;
        float timeSinceLastChange;

        float actualAng = 0;
        [Range(0f,360f)]
        public float angularThreshold;

        Direccion actualDir;

        public override void Start(){
            timeSinceLastChange = timeToChange;
        }

        public override Direccion GetDirection(){
           //Timer to know when to change direction
            if (timeSinceLastChange >= timeToChange){
                Direccion direccion = new Direccion();
                direccion.angular = Random.Range(0, 361);

                //While to check the rat doesnt change direction too abruptly
                while (direccion.angular < actualAng + angularThreshold && direccion.angular > actualAng - angularThreshold) 
                    direccion.angular = Random.Range(0, 361);

                //Giving direction in function of the orientation of the rat
                direccion.lineal = OriToVec(direccion.angular);
                direccion.lineal.Normalize();
                direccion.lineal *= agente.aceleracionMax;

                timeSinceLastChange = 0;
                actualDir = direccion;

                //Radom to make the movement more erratic
                timeToChange = Random.Range(1, 1.7f);
            }
            else
                timeSinceLastChange += Time.deltaTime;

            return actualDir;
        }
    }
}
