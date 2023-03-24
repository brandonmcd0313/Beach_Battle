using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleEnd : MonoBehaviour {
    //the castle will control the end game and highscores
    //why? becuase why not!
    public Canvas gameCanvas; //quarantine the sticks
    public Canvas endCanvas;  //same goes for sand  
    public int highScore = 0;
    bool ended = false;
    string highScoreKey = "HighScore";
    public Text scoreText2; public Text highScoreText;
    public AudioClip winNoise;
    public float winVol;
    // Use this for initialization
    void Start () {
        Screen.SetResolution(640, 360, false);
        gameCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        gameCanvas.worldCamera = Camera.main;
        //Get the highScore from player prefs if it is there, 0 otherwise.
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        endCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        endCanvas.worldCamera = Camera.main;
        endCanvas.enabled= false;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndGame()
    {
        if(!ended)
        {
            //play winNoise
            GetComponent<AudioSource>().PlayOneShot(winNoise, winVol);
            //destroy crabwalkin
            GameObject.Find("EventManager").GetComponent<EndlessManager>().killAud();
            ended = true;
            GetComponent<SpriteRenderer>().color = new Color(221, 109, 157);
            gameCanvas.enabled = false;
            //If our scoree is greter than highscore, set new higscore and save.
            if (PlayerController.score > highScore)
        {
             PlayerPrefs.SetInt(highScoreKey, PlayerController.score);
             PlayerPrefs.Save();
            }
            endCanvas.enabled = true;
        scoreText2.text = "Your Score: " + PlayerController.score;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(highScoreKey);
        }
    }

    public void newGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
