using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver = false;
    public float step = 2;
    public float stepSpeed = 0.5f;
    public float xLimit = 11;
    public float spawnSpeed = 0;

    public AudioClip moveSound;
    public AudioClip deathSound;
    public AudioClip goalSound;

    public ParticleSystem explosion;

    private Vector3 min;
    private TextController text;
    private Rigidbody playerRB;
    private AudioSource playerAudio;
    private Animator playerAnime;
    private LevelManager lm;
    private Vector3 toBe;
    private Quaternion toLook;
    private float horizontalInputs, verticalInpunts, fire1;
    private bool canGo = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        toBe = transform.position;
        min = toBe;
        toLook = transform.rotation;
        lm = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        playerAnime = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        text = GameObject.Find("Text").GetComponent<TextController>();
        Invoke("noGo", stepSpeed/2);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInputs = Input.GetAxis("Horizontal");
        verticalInpunts = Input.GetAxis("Vertical");
        fire1 = Input.GetAxis("Reset");

        if (!gameOver)
        {
            transform.position = ((toBe - transform.position) / 10) + transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toLook, 10);
        }
        playerAnime.SetFloat("Speed_f", Mathf.Abs(Vector3.Distance(toBe, transform.position)));
        if (canGo && !gameOver && !playerAnime.GetBool("Sit_b"))
        {
            if (verticalInpunts > 0)
            {
                toBe.z += step;
                playerAudio.PlayOneShot(moveSound, 1f);
                canGo = false;
                Invoke("resetGo", stepSpeed);
                toLook = Quaternion.AngleAxis(0, Vector3.up);
            }
            if (verticalInpunts < 0)
            {
                toBe.z -= step;
                playerAudio.PlayOneShot(moveSound, 1f);
                canGo = false;
                Invoke("resetGo", stepSpeed);
                toLook = Quaternion.AngleAxis(180, Vector3.up);
            }
            if (horizontalInputs < 0)
            {
                toBe.x -= step;
                playerAudio.PlayOneShot(moveSound, 1f);
                canGo = false;
                Invoke("resetGo", stepSpeed);
                toLook = Quaternion.AngleAxis(-90, Vector3.up);
            }
            if (horizontalInputs > 0)
            {
                toBe.x += step;
                playerAudio.PlayOneShot(moveSound, 1f);
                canGo = false;
                Invoke("resetGo", stepSpeed);
                toLook = Quaternion.AngleAxis(90, Vector3.up);
            }
        }

        if (toBe.x > xLimit)
        {
            toBe.x = xLimit;
        }
        if (toBe.x < -xLimit)
        {
            toBe.x = -xLimit;
        }
        if (toBe.z < min.z) {
            toBe.z = min.z;
        }

        if (fire1 > 0 && gameOver)
        {
            lm.Level = 0;
            lm.NextLevel();
            toBe = transform.position;
            min = toBe;
            canGo = false;
            Invoke("noGo", stepSpeed/2);
            gameOver = false;
            playerRB.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Thing")) {
            gameOver = true;
            text.hold("Game Over! Compleated " + (lm.Level-1) + " Levels!");
            explosion.Play();
            playerAudio.PlayOneShot(deathSound, 1f);
            playerAnime.SetBool("Sit_b", true);
            playerRB.velocity = Vector3.zero;
        } else if (other.gameObject.CompareTag("Goal")) {
            text.set("Level " + lm.Level + " Passed!");
            lm.NextLevel();
            toBe = transform.position;
            min = toBe;
            canGo = false;
            Invoke("noGo", stepSpeed/2);
            playerAudio.PlayOneShot(goalSound, 1f);
            playerRB.velocity = Vector3.zero;
        }
        //Debug.Log(other.gameObject.tag);
    }

    void resetGo() {
        canGo = true;
    }
    void letsGo()
    {
        Invoke("resetGo", 4);
        playerAnime.SetBool("Sit_b", false);
        text.set("");
    }
    void noGo()
    {
        Invoke("letsGo", spawnSpeed);
        canGo = false;
        playerAnime.SetBool("Sit_b", true);
    }
}
