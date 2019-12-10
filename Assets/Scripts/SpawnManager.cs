using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] preFabs;
    public GameObject spawnPointOffsetLeft;
    public GameObject spawnPointOffsetRight;
    public float spawnFrequency = 100;
    public float distance = 30;

    private GameObject[] preFabPool;
    private int index = 0;
    private int poolSize = 20;

    private float spawnRate = 1;
    private PlayerController playerControllerScript;
    private GameObject theCamera;

    // Start is called before the first frame update
    void Start()
    {
        preFabPool = new GameObject[poolSize];
        for(int i = 0; i < poolSize; i++) {
            preFabPool[i] = (GameObject)Instantiate(preFabs[Random.Range(0, preFabs.Length)]);
            preFabPool[i].SetActive(false);
        }
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        theCamera = GameObject.Find("Main Camera");
        InvokeRepeating("SpawnObstacle", 0, spawnRate);
    }

    void SpawnObstacle() {
        float random = Random.Range(0, 100);
        if (random <= spawnFrequency && !playerControllerScript.gameOver && Mathf.Abs(theCamera.transform.position.z - transform.position.z) < distance)
        {
            //int temp = Random.Range(0, preFabs.Length);
            //Instantiate(preFabs[temp], spawnPointOffsetLeft.transform.position, spawnPointOffsetLeft.transform.rotation);
            WakeObstacle(spawnPointOffsetLeft.transform.position, spawnPointOffsetLeft.transform.rotation);
        }

        random = Random.Range(0, 100);
        if (random <= spawnFrequency && !playerControllerScript.gameOver && Mathf.Abs(theCamera.transform.position.z - transform.position.z) < distance)
        {
            //int temp = Random.Range(0, preFabs.Length);
            //Instantiate(preFabs[temp], spawnPointOffsetRight.transform.position, spawnPointOffsetRight.transform.rotation);
            WakeObstacle(spawnPointOffsetRight.transform.position, spawnPointOffsetRight.transform.rotation);
        }
    }

    public void DrainPool() {
        for(int i = 0; i < poolSize; i++) {
            preFabPool[i].SetActive(true);
            Destroy(preFabPool[i]);
        }
    }

    void WakeObstacle(Vector3 pos, Quaternion rotat) {
        preFabPool[index].SetActive(true);
        preFabPool[index].transform.position = pos;
        preFabPool[index].transform.rotation = rotat;
        index++;
        if (index >= poolSize) {
            index = 0;
        }
    }
}
