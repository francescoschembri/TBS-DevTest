using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public AudioClip SFXPickup;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(SFXPickup, Camera.main.transform.position);
        Done_PlayerController player = other.GetComponent<Done_PlayerController>();
        if (player)
        {
            player.ActiveShield();
        }
        Destroy(gameObject);
    }
}
