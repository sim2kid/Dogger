using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSpawnManager : MonoBehaviour
{
    public GameObject[] preFabs;
    public float spawnDensity = 100;
    public float xLimit = 20;
    public float step = 2;
    public Vector3 offset = new Vector3(1,0,0);

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        int clear = Random.Range(1, 12);
        for (float i = transform.position.x - (xLimit+step); i <= transform.position.x + xLimit; i += step) {
            for (float b = transform.position.z - (step / 2); b <= transform.position.z + (step / 2); b += step) {
                float rand = Random.Range(0, 100);
                if (rand <= spawnDensity && clear + 5 != count) {
                    int temp = Random.Range(0, preFabs.Length);
                    Instantiate(preFabs[temp], new Vector3(i + offset.x, transform.position.y + offset.y, b + offset.z), preFabs[temp].transform.rotation);
                }
            }
            count++;
        }
    }
}
