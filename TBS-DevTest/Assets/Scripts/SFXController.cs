using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameController gameController;
    private float startVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        startVolume = audioSource.volume;
        ManageVolume();
    }

    private float ManageVolume()
    {
        float newVolume = audioSource.volume * gameController.GetSFXMultiplier();
        if (newVolume > Mathf.Epsilon)
            audioSource.volume = newVolume;
        else
            audioSource.volume = 0;
        return audioSource.volume;
    }

    private void Update()
    {
        ManageVolume();
    }
}
