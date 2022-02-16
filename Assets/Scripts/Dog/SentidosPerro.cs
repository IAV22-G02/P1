using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class SentidosPerro : MonoBehaviour
    {
        public float ratThreshold;

        dogMovement dogMov;

        List<GameObject> rats;
        public void Start()
        {
            dogMov = transform.parent.gameObject.GetComponent<dogMovement>();
            rats = dogMov.getRats();
        }


        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.GetComponent<RataSenses>() != null)
            {
                rats.Add(c.gameObject);
            }
        }
        void OnTriggerExit(Collider c)
        {
            if (c.gameObject.GetComponent<RataSenses>() != null)
            {
                if (rats.Contains(c.gameObject))
                {
                    rats.Remove(c.gameObject);
                }
            }
        }
    }
}