using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5;
    public float XLimit = 30;

    private PlayerController playerControllerScript;
    private Animator thingAm;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        thingAm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.position.x > XLimit || transform.position.x < -XLimit)
            {
                gameObject.SetActive(false);
            }
        }
        else if (thingAm != null)
        {
            thingAm.SetFloat("Speed_f", 0);
        }
    }
}
