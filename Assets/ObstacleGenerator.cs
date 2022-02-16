using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] obstacles;

    [SerializeField]
    int maxX;
    [SerializeField]
    int minX;
    [SerializeField]
    int maxZ;
    [SerializeField]
    int minZ;

    [Range(0.0f, 1.0f)]
    public float probability;

    public float scale = 20.0f;

    void Start(){
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GenerateObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateObstacle(){
        for (int i = (int) minX; i < maxX; i++){
            for (int j = minZ; j < maxZ; j++){
                float prob = CalculateHeight(i, j);

                if(prob  < probability){
                    int g = Random.Range(0, obstacles.Length);
                    GameObject o = Instantiate(obstacles[g], new Vector3(i, 0.25f, j), Quaternion.identity);
                    o.transform.parent = this.gameObject.transform;
                    o.layer = LayerMask.NameToLayer("Obstaculo");
                }
            }
        }
    }

    float CalculateHeight(int x, int y) {
        float xCoord = (float)x / (maxX - minX);

        xCoord += Random.Range(0.0f, 0.05f);
        float yCoord = (float)y / (maxZ - minZ);
        yCoord += Random.Range(0.0f, 0.05f);

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
