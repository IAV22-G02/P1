using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento {
    public class Wander : ComportamientoAgente{
        [Range(0f,360f)]
        public float angularThreshold;

        float timeToChange = 0.4f;
        float auxFactor = 0.7f;
        float timeSinceLastChange;
        Direccion direction;
        bool change;
        
        public override void Start(){
            timeSinceLastChange = 0;
            change = false;
            direction = new Direccion();
            //Starts off with random ori
            direction.orientation = Random.Range(0, 361);
        }

        public override Direccion GetDirection(){
            //Decidir si sumar o no
            int changeProb = Random.Range(0, 2000);
            if(changeProb <= 2 && !change){
                change = true;
                int addProb = Random.Range(0, 11);
                //Decidir si sumar o restar
                if (addProb <= 5) auxFactor = auxFactor * -1;

                timeToChange = Random.Range(0.4f, 0.7f);
                timeSinceLastChange = 0;
            }

            //Sumar
            if (change && timeSinceLastChange <= timeToChange){
                timeSinceLastChange += Time.deltaTime;
                direction.orientation += auxFactor;
            }
            else change = false;
       
            //Dar direccion
            direction.lineal = OriToVec(direction.orientation);
            direction.lineal.Normalize();

            if(agente != null)
                direction.lineal *= agente.aceleracionMax;

            return direction;
        }
    }
}
