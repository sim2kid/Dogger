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

    private float spawnRate = 1;
    private PlayerController playerControllerScript;
    private GameObject theCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        theCamera = GameObject.Find("Main Camera");
        InvokeRepeating("SpawnObstacle", 0, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle() {
        float random = Random.Range(0, 100);
        if (random <= spawnFrequency && !playerControllerScript.gameOver && Mathf.Abs(theCamera.transform.position.z - transform.position.z) < distance)
        {
            int temp = Random.Range(0, preFabs.Length);
            Instantiate(preFabs[temp], spawnPointOffsetLeft.transform.position, spawnPointOffsetLeft.transform.rotation);
        }

        random = Random.Range(0, 100);
        if (random <= spawnFrequency && !playerControllerScript.gameOver && Mathf.Abs(theCamera.transform.position.z - transform.position.z) < distance)
        {
            int temp = Random.Range(0, preFabs.Length);
            Instantiate(preFabs[temp], spawnPointOffsetRight.transform.position, spawnPointOffsetRight.transform.rotation);
        }
    }
}
