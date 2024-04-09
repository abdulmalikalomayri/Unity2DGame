using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
            
            } 

        }
        
}
