using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class SentidosPerro : Seguir
    {
        public float ratThreshold;
        int ratsInRange;
        //List<GameObject> ratsOfFear;
        bool huyendo = false;

        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.GetComponent<RataSenses>() != null)
            {
                ratsInRange++;

                if (ratsInRange >= ratThreshold)
                {
                    this.transform.parent.GetComponent<Seguir>().enabled = false;
                    huyendo = true;
                }

                //ratsOfFear.Add(c.gameObject);
            }
        }
        void OnTriggerExit(Collider c)
        {
            if (c.gameObject.GetComponent<RataSenses>() != null)
            {
                ratsInRange--;

                if (ratsInRange < ratThreshold)
                {
                    this.transform.parent.GetComponent<Seguir>().enabled = true;
                    huyendo = false;
                }
                //ratsOfFear.Add(c.gameObject);
            }
        }
        public override Direccion GetDirection()
        {
            Direccion direccion = new Direccion();
            direccion.lineal = -(Separate());
            direccion.lineal.Normalize();
            direccion.lineal *= agente.aceleracionMax;
            return direccion;
        }
    }
}