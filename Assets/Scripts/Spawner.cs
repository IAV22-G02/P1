using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Spawner : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField]
        int numMaxRats = 0;

        [SerializeField]
        GameObject objectToSpawn;

        [Header("Probabilidad (%)")]
        [Range(0, 100)]
        public int probability;

        static uint currNumRats = 0;
        int maxNum = 1001;

        List<GameObject> rats;

        float timeLimit = 4.0f;
        float timer;


        int prob;
        public void Start(){
            currNumRats = 0;
            timer = 0;
            rats = SensorialManager.instance.getRats();
            rats.Clear();
            prob = (probability / 100) * maxNum;
        }

        // Update is called once per frame
        void Update() {
            int auxNum = Random.Range(0, maxNum);

            if (auxNum <= prob && currNumRats < numMaxRats) {
                createRat();
            }

            if (timer >= timeLimit && currNumRats < numMaxRats){
                createRat();
                timer = 0;
            }
            else timer += Time.deltaTime;

        }
        private void createRat() {
            GameObject rat = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity);
            rat.transform.parent = gameObject.transform;

            rats.Add(rat);
            currNumRats++;
        }
    }

}
