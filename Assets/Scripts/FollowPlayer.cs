using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offSet = new Vector3(0,0,0);
    public bool Following = false;
    public float panSpeed = 10;
    private GameObject player;
    public float spawnZ;

    private bool temp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        temp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - player.transform.position.z < offSet.z + 0.3) //if cam is behind the player or infront by .3//
        {
            if (temp == false) {
                //TRIGGER
                GameObject.Find("GameManager").GetComponent<GameManager>().startCounter(spawnZ - player.transform.position.z);

                temp = true;
            }
            transform.position = new Vector3(offSet.x, offSet.y, player.transform.position.z + offSet.z); //cam follows the player//
            Following = true;
        }
        else { //else level panning//
            temp = false;
            Following = false;
            float speed = (spawnZ - player.transform.position.z)/panSpeed;
            if ((transform.position.z - (player.transform.position.z + offSet.z)) < 16)
            {
                speed = speed * ((transform.position.z - (player.transform.position.z + offSet.z)) / 16);
            }
            else if ((spawnZ - transform.position.z) < 20)
            {
                speed = speed * ((spawnZ - transform.position.z) / 20);
            }
            speed++;
            //Debug.Log((spawnZ - transform.position.z) + " " + (transform.position.z - (player.transform.position.z + offSet.z)) + " " + (speed));
            transform.position = new Vector3(transform.position.x, offSet.y, transform.position.z - (speed * Time.deltaTime));
        }
        
    }
}
