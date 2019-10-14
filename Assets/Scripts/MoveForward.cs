using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5;
    public float XLimit = 30;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.position.x > XLimit || transform.position.x < -XLimit)
            {
                Destroy(gameObject);
            }
        }
    }
}
