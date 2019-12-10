using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] ActiveLanes;
    public GameObject[] PassiveLanes;
    public GameObject StartLane;
    public GameObject EndLane;
    public float step = 2;

    public int Level = 0;
    // Start is called before the first frame update
    void Start()
    {
        NextLevel();
    }

    public void NextLevel() {
        Level++;
        ResetLevel();
        SetUpLevel();
    }

    public void NewGame(int level)
    {
        Level = level;
        ResetLevel();
        SetUpLevel();
    }

    void ResetLevel() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SpawnManager");
        for (int i = 0; i < objs.Length; i++)
        {
            SpawnManager sm = objs[i].GetComponent<SpawnManager>();
            sm.DrainPool();
        }

        objs = GameObject.FindGameObjectsWithTag("Lane");
        for (int i = 0; i < objs.Length; i++) {
            Destroy(objs[i]);
        }

        objs = GameObject.FindGameObjectsWithTag("Thing");
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
    }

    void SetUpLevel() {
        int rounds = (int)Math.Round(Math.Sin((Level + 1f) * 2f) + Level) + 1;
        float ZPos = 0;

        Instantiate(StartLane, new Vector3(0, 0, ZPos), StartLane.transform.rotation);
        ZPos += step * 2;
        for (int i = 0; i < rounds; i++)
        {
            int mod = UnityEngine.Random.Range(1, Level) -1;
            for (int b = 0; b < (i + mod) % 4 + 1; b++)
            {
                mod = UnityEngine.Random.Range(1, Level) -1;
                int rand = UnityEngine.Random.Range(0, ActiveLanes.Length);
                Instantiate(ActiveLanes[rand], new Vector3(0, 0, ZPos), ActiveLanes[rand].transform.rotation);
                float frequency = (float)((70f * 2f) * (1f / (1f + Math.Pow(1.03f, -(Level + (i + b) - 1f)))) - 45f);
                ActiveLanes[rand].transform.Find("SpawnManager").GetComponent<SpawnManager>().spawnFrequency = frequency;
                ZPos += step * 2;
            }
            int type = UnityEngine.Random.Range(0, PassiveLanes.Length);

            Instantiate(PassiveLanes[type], new Vector3(0, 0, ZPos), PassiveLanes[type].transform.rotation);
            float dens = (float)((35f * 2f) * (1f / (1f + Math.Pow(1.03f, -(Level + i - 1f)))) - 5f);
            PassiveLanes[type].transform.Find("SpawnManager").GetComponent<RestSpawnManager>().spawnDensity = dens;

            ZPos += step * 2;
        }
        Instantiate(EndLane, new Vector3(0, 0, ZPos), EndLane.transform.rotation);

        GameObject player = GameObject.Find("Player");
        GameObject camera = GameObject.Find("Main Camera");
        Vector3 spawnPoint = GameObject.Find("SpawnPoint").transform.position;

        player.transform.position = spawnPoint;
        Invoke("camUpdate", 0.01f);
    }

    void camUpdate() {
        GameObject camera = GameObject.Find("Main Camera");
        Vector3 camSpawn = GameObject.Find("CamSpawn").transform.position;
        camera.transform.position = camSpawn;
        camera.GetComponent<FollowPlayer>().spawnZ = camSpawn.z;
    }
}
