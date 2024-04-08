using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    
    [SerializeField] float levelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(LoadNextLevel()); // wait a memoment before loadin the next level

        if(other.tag == "Player")
        {
            Debug.Log("Player reached the exit");
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay); // here it will pause for 1 second

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // store the current scene index
        Debug.Log(currentSceneIndex);

        SceneManager.LoadScene(currentSceneIndex + 1); // load the next scene
    }

}
