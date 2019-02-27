using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField] AudioClip SFXPickup;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(SFXPickup,
                                    Camera.main.transform.position,
                                    FindObjectOfType<GameController>().GetSFXMultiplier());
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
        {
            player.ActiveShield();
        }
        Destroy(gameObject);
    }
}
