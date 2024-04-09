using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 100;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !wasCollected){
            wasCollected = true;

            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);

            gameObject.SetActive(false);

            FindObjectOfType<GameSession>().AddToScore(coinValue);
            } 

        }
        
}
