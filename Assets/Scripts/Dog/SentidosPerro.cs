using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class SentidosPerro : MonoBehaviour
    {
        public float ratThreshold;
        int ratsInRange;
        
        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.GetComponent<RataSenses>() != null)
            {
                ratsInRange++;

                if (ratsInRange >= ratThreshold)
                {
                    this.gameObject.GetComponent<Seguir>().enabled = false;
                    //? this.gameObject.GetComponent<Huir>().enabled = true;
                }
            }
        }
        void OnTriggerExit(Collider c)
        {
            if (c.gameObject.GetComponent<RataSenses>() != null)
            {
                ratsInRange--;

                if (ratsInRange < ratThreshold)
                {
                    this.gameObject.GetComponent<Seguir>().enabled = true;
                    //? this.gameObject.GetComponent<Huir>().enabled = false;
                }
            }
        }
    }
}