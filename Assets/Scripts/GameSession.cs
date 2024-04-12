using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI ScoreText;

    // Awake method is called before Start method, it's the first method called
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        ScoreText.text = score.ToString();
        
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreText.text = score.ToString();
    }
    

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1) 
        {
            TakeLife();
        }
        else 
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist(); // reset the scene persist
        SceneManager.LoadScene(0); // go to the first scene 
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

}
