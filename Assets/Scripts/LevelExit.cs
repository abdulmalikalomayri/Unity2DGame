using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 0f;
    
    [SerializeField] AudioClip winSound;

    void OnTriggerEnter2D(Collider2D other) 
    {        
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
            
            // freeze the player 
            other.GetComponent<Animator>().SetTrigger("Dying");
            other.GetComponent<PlayerMove>().freezePlayer();
            other.GetComponent<PlayerMove>().enabled = false;

            


            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
