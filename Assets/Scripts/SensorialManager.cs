using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;


namespace UCM.IAV.Movimiento
{
    public class SensorialManager : MonoBehaviour
    {
        public static SensorialManager instance;

        List<GameObject> rats;

        GameObject target;
        bool isFlauta = false;

        [SerializeField]
        Text ratsN;
        [SerializeField]
        Text fps;

        // Variables para el frameRate
        int contadoFrames = 0;
        float contadorDeTiempo = 0.0f;
        float ultimaVez = 0.0f;
        float tiempoDeRefresco = 0.5f;

        private void Awake()
        {
            rats = new List<GameObject>();
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
            //Capar el juego a 60 para que no revienten las métricas
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (contadorDeTiempo < tiempoDeRefresco)
            {
                contadorDeTiempo += Time.deltaTime;
                contadoFrames++;
            }
            else
            {
                ultimaVez = (float)contadoFrames / contadorDeTiempo;
                contadoFrames = 0;
                contadorDeTiempo = 0.0f;
            }

            // Escibimos el ratio de frame
            fps.text = "Frames:" + (((int)(ultimaVez * 100 + .5) / 100.0)).ToString();

            updateRatText();
        }

        public void updateRatText()
        {
            ratsN.text = "Rats:" + rats.Count.ToString();
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

        public ref List<GameObject> getRats(){
            return ref rats;
        }

    }
}
