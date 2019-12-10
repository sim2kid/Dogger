using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Playable = false;
    public GameObject EndCard, TitleCard, EndLevelCard, ScoreCard;

    private float levelScore;
    private float Score;

    private PlayerController pc;
    private LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        TitleCard.SetActive(true);
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        lm = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    private void CamPan() {
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.GetComponent<FollowPlayer>().spawnZ);
    }

    public void GameOver(int LevelsPassed) {
        EndCard.SetActive(true);
        GameObject.Find("Death Text").GetComponent<TextMeshProUGUI>().SetText("You Died!\nYou Completed\n{0} Levels", LevelsPassed);
        Playable = false;
        breakCounter();
    }

    public void LevelPassed(int LastLevel)
    {
        EndLevelCard.SetActive(true);
        GameObject.Find("Level End Text").GetComponent<TextMeshProUGUI>().SetText("Completed\nLevel {0}", LastLevel);
        Playable = false;
        endCounter();
    }



    public void buttonNextLevel() {
        EndLevelCard.SetActive(false);
        lm.NextLevel();
        pc.backToSpawn();
        Playable = true;
    }

    public void buttonStart() {
        Score = 0;
        levelScore = 0;
        ScoreCard.SetActive(true);
        endScore();
        TitleCard.SetActive(false);
        lm.NewGame(1);
        pc.backToSpawn();
        Playable = true;
    }

    public void buttonRestart()
    {
        EndCard.SetActive(false);

        ScoreCard.SetActive(false);
        CamPan();

        TitleCard.SetActive(true);
    }



    public void startCounter(float distance)
    {
        levelScore = distance * 2;
        StartCoroutine(countDown());
    }

    private IEnumerator countDown() {
        while (Playable && levelScore > 0) {
            levelScore -= 0.1f;
            updateScore();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void endCounter()
    {
        Score += levelScore;
        endScore();
    }

    public void breakCounter()
    {
        endScore();
    }

    private void endScore() {
        ScoreCard.GetComponent<TextMeshProUGUI>().SetText("Score: {0:1}", Score);
    }

    private void updateScore() {
        ScoreCard.GetComponent<TextMeshProUGUI>().SetText("Score: {0:1}\nLevel {2:0}: +{1:1}", Score, levelScore, lm.Level);
    }

    public void buttonExit() {
        Application.Quit();
    }
}
