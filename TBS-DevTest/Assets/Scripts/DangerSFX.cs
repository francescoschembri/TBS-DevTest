using UnityEngine;

public class DangerSFX : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnDisable()
    {
        GetComponent<AudioSource>().Stop();
    }
}
