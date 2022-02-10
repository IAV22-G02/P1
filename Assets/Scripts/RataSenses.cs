using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento
{
    public class RataSenses : MonoBehaviour
    {
        bool isFollowing = false;
        private void Update()
        {

            if (!isFollowing && SensorialManager.instance.getFlautaTravesera())
            {
                this.gameObject.GetComponent<Seguir>().enabled = true;
                this.gameObject.GetComponent<Wander>().enabled = false;
                isFollowing = true;
            }
            else if (isFollowing && !SensorialManager.instance.getFlautaTravesera())
            {
                this.gameObject.GetComponent<Seguir>().enabled = false;
                this.gameObject.GetComponent<Wander>().enabled = true;
                isFollowing = false;
            }
        }
    }
}