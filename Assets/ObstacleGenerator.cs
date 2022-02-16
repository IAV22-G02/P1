using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] obstacles;

    public GameObject terrain;

    int minX, minZ, maxX, maxZ;

    [Range(0.0f, 1.0f)]
    public float probability;

    void Start(){
        minX = (int)terrain.GetComponent<MeshCollider>().bounds.min.x;
        minZ = (int)terrain.GetComponent<MeshCollider>().bounds.min.z;
        maxX = (int)terrain.GetComponent<MeshCollider>().bounds.max.x;
        maxZ = (int)terrain.GetComponent<MeshCollider>().bounds.max.z;
        GenerateObstacle();
    }


    void GenerateObstacle(){
        for (int i = (int) minX; i < maxX; i++){
            for (int j = minZ; j < maxZ; j++){
                float prob = CalculateHeight(i, j);

                if(prob  < probability){
                    int g = Random.Range(0, obstacles.Length);
                    GameObject o = Instantiate(obstacles[g], new Vector3(i, 0, j), Quaternion.identity);
                    o.transform.parent = this.gameObject.transform;
                    o.layer = LayerMask.NameToLayer("Obstaculo"); 
                }
            }
        }
    }

    float CalculateHeight(int x, int y) {



        return Mathf.PerlinNoise(x * 0.03f, y *0.03f);
    }
}
