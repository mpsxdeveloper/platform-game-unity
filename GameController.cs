using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public Text scoreText;
    public Text livesText;
    public Text coinsText;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

        /* PlayerPrefs.DeleteAll(); */
        audioSource = GetComponent<AudioSource>();
        if(PlayerPrefs.HasKey("TOTAL_SCORE")) {
            Global.totalScore = PlayerPrefs.GetInt("TOTAL_SCORE");
        }
        else {
            PlayerPrefs.SetInt("TOTAL_SCORE", Global.totalScore);
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("TOTAL_LIVES")) {
            Global.totalLives = PlayerPrefs.GetInt("TOTAL_LIVES");
        }
        else {
            PlayerPrefs.SetInt("TOTAL_LIVES", Global.totalLives);
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.HasKey("TOTAL_COINS")) {
            Global.totalCoins = PlayerPrefs.GetInt("TOTAL_COINS");
        }
        else {
            PlayerPrefs.SetInt("TOTAL_COINS", Global.totalCoins);
            PlayerPrefs.Save();
        }
        instance = this;
        UpdateLivesText();
        UpdateCoinsText();
        UpdateScoreText();
    }

    public void UpdateScoreText() {
        scoreText.text = Global.totalScore.ToString();
    }

    public void UpdateLivesText() {
        livesText.text = "X " + Global.totalLives.ToString();
    }

    public void UpdateCoinsText() {
        coinsText.text = "X " + Global.totalCoins.ToString();
    }

    public void UpdateScore(int value) {        
        PlayerPrefs.SetInt("TOTAL_SCORE", Global.totalScore+=value);
        PlayerPrefs.Save();
        UpdateScoreText();    
    }

    public void UpdateCoins() {  
        PlayerPrefs.SetInt("TOTAL_COINS", Global.totalCoins++);
        PlayerPrefs.SetInt("TOTAL_SCORE", Global.totalScore+=100);
        PlayerPrefs.Save();
        if(Global.totalCoins == 100) {
            Global.totalCoins = 0;
            Global.totalLives++;
            PlayerPrefs.SetInt("TOTAL_LIVES", Global.totalLives);
            PlayerPrefs.SetInt("TOTAL_COINS", Global.totalCoins);
            PlayerPrefs.Save();
        }        
        UpdateCoinsText();
        UpdateScoreText();
        UpdateLivesText();
    }

    public void LostLive() {
        Global.totalLives--; 
        PlayerPrefs.SetInt("TOTAL_LIVES", Global.totalLives);
        PlayerPrefs.Save();
        UpdateLivesText();
        if(Global.totalLives == 0) {
            ShowGameOver();
        }
        else {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void ShowGameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame(string lvlName) {
        lvlName = "Level_1";
        Global.totalLives = 3;
        Global.totalScore = 0;
        SceneManager.LoadScene(lvlName);
    }

    public void PlayCoinSound() {
        audioSource.Play();
    }

}