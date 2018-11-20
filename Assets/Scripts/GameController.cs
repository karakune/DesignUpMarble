using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    private GameObject player;
    private int score;
    private Text scoreText;
    private Text gameStatusText;

    private Vector3 respawnPoint;
    private Vector3 initialSpawnPoint;
    private Vector3 initialCameraOffset;
    private bool firstLoad = true;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += SetupLevel;
        SetupLevel();
    }

    private void SetupLevel(Scene scene, LoadSceneMode mode)
    {
        SetupLevel();
    }

    private void SetupLevel()
    {
        score = 0;

        // Get the UI elements, if they exist, then set them up
        GameObject scoreGameObject = GameObject.Find("ScoreText");
        if (scoreGameObject != null)
        {
            scoreText = scoreGameObject.GetComponent<Text>();
            UpdateScoreText();
        }

        GameObject gameStatusGameObject = GameObject.Find("GameStatusText");
        if (gameStatusGameObject != null)
        {
            gameStatusText = gameStatusGameObject.GetComponent<Text>();
            gameStatusText.gameObject.SetActive(false);
        }

        // Get the player
        player = GameObject.Find("Player");

        // Keep track of the player's initial spawn point
        if (firstLoad)
        {
            initialSpawnPoint = player.transform.position;
            SetRespawnPoint(initialSpawnPoint);
            firstLoad = false;

            // Make sure the perspective won't change after a checkpoint
            initialCameraOffset = Camera.main.transform.position - initialSpawnPoint;
        }

        // Put the player at his last checkpoint
        player.transform.position = respawnPoint;
        Camera.main.transform.position = player.transform.position + initialCameraOffset;

        // Resume the game
        Time.timeScale = 1f;
    }

    public void CompleteLevel()
    {
        // Show the victory message
        gameStatusText.text = "Level completed! Congrats!";
        gameStatusText.gameObject.SetActive(true);

        // Place the player back where he started
        SetRespawnPoint(initialSpawnPoint);

        GameOver();
    }

    public void Lose()
    {
        gameStatusText.text = "You died. Try again.";
        gameStatusText.gameObject.SetActive(true);
        GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        StartCoroutine(ResetAfterSeconds(5));
    }

    private IEnumerator ResetAfterSeconds(int seconds)
    {
        float pauseEndTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null; // Waits one frame
        }

        ResetLevel();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetRespawnPoint(Vector3 respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }

    public void AddPoints(int points)
    {
        this.score += points;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
