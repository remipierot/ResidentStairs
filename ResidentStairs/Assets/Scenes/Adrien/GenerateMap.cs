using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    public GameObject obj;

    public int xSize = 30;
    public int zSize = 30;

    // Use this for initialization
    void Start () {
        for (int x = -xSize; x < xSize; x++)
        {
            for (int z = -zSize; z < zSize; z++)
            {
                GameObject o1 = (GameObject)Instantiate(obj, new Vector3(x*10f + Random.Range(0f, 3f), 0.55f, z*10f + Random.Range(0f, 3f)), Quaternion.identity);
                Vector3 o1Pos = o1.gameObject.transform.position;
                GameObject o2 = (GameObject)Instantiate(obj, new Vector3(o1Pos.x + Random.Range(-3f, 3f), 0.55f, o1Pos.z + Random.Range(-3f, 3f)), Quaternion.identity);
                GameObject o3 = (GameObject)Instantiate(obj, new Vector3(o1Pos.x + Random.Range(-3f, 3f), 0.55f, o1Pos.z + Random.Range(-3f, 3f)), Quaternion.identity);

                Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
                o1.transform.localScale = scale;
                o2.transform.localScale = scale;
                o3.transform.localScale = scale;
            }
        }
    }
}
