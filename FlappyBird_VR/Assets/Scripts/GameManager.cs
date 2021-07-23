using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float waitingTime = 1.5f;
    public bool ready = true;
    public GameObject cactus;
    public bool end = false;
    public GameObject bird;

    public AudioClip deathSound;
    public AudioClip goalSound;
    public int score;
    public TextMesh scoreText;

    public GameObject readyimg01;
    public GameObject readyimg02;
    public GameObject gameOverimg;
    public GameObject finalWindow;
    public GameObject imageNew;
    public TextMesh finalScoreText;
    public TextMesh bestScoreTest;

    private void Start()
    {
        PlayerPrefs.SetInt("BS",0);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&ready == true)
        {
            ready = false;
            InvokeRepeating("MakeCactus", 1f, waitingTime);
            bird.GetComponent<Rigidbody>().useGravity = true;
            iTween.FadeTo(readyimg01, iTween.Hash("alpha", 0, "time", 0.5));
            iTween.FadeTo(readyimg02, iTween.Hash("alpha", 0, "time", 0.5));
        }
        
    }
    public void GameOver()
    {
        if (end == true) return;
        end = true;
        CancelInvoke("MakeCactus");
        iTween.ShakePosition(Camera.main.gameObject,
            iTween.Hash("x", 0.2, "y", 0.2, "time", 0.5f));
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        iTween.FadeTo(gameOverimg, iTween.Hash("alpha", 255, "delay", 1f, "time", 0.5f));
        iTween.MoveTo(finalWindow, iTween.Hash("y", 1, "delay", 1.3f, "time", 0.5f));

        if(score>PlayerPrefs.GetInt("BS"))
        {
            PlayerPrefs.SetInt("BS", score);
            imageNew.SetActive(true);
        }else if(score <= PlayerPrefs.GetInt("BS"))
        {
            imageNew.SetActive(false);
        }
        finalScoreText.text = score.ToString();
        bestScoreTest.text = PlayerPrefs.GetInt("BS").ToString();
    }
    public void GetScore()
    {
        score += 1;
        scoreText.text = score.ToString();
        AudioSource.PlayClipAtPoint(goalSound, transform.position);
    }
    void MakeCactus()
    {
        Instantiate(cactus);
    }
}
