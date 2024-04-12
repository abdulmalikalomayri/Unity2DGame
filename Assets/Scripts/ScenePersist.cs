using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Awake method is called before Start method, it's the first method called
    void Awake()
    {
        // below logic is to keep eniemies and coins from respawning when the player dies
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
