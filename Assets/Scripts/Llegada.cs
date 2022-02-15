using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Llegada : ComportamientoAgente
    {
        List<GameObject> target;
        Rigidbody rb;

        float maxAcceleration;
        float maxVelocity;

        //Radius for arriving at the target
        float targetRadius;

        //Radius for beginning to slow down
        float slowRadius;

        //Time over which to achieve target speed
        float timeToTarget = 0.1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}