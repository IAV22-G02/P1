using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Start is called before the first frame update

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

    public LayerMask layer;
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
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Vector3 pos = cube.transform.position;
                    pos.y = 0.5f;
                    pos.x = i;
                    pos.z = j;
                    cube.transform.position = pos;
                    cube.transform.parent = this.gameObject.transform;
                    cube.layer = LayerMask.NameToLayer("Obstaculo");
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
