using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento
{
    public class SensorialManager : MonoBehaviour
    {
        public static SensorialManager instance;

        GameObject target;
        bool isFlauta = false;


        private void Awake()
        {
            //Cosa que viene en los apuntes para que el gamemanager no se destruya entre escenas
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        public GameObject getTarget()
        {
            return target;
        }

        public void setTarget(GameObject t)
        {
            target = t;
        }

        public void PlayFlauta(bool isPlaying)
        {
            isFlauta = isPlaying;
        }
        public bool getFlautaTravesera()
        {
            return isFlauta;
        }
    }
}
