using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public AudioClip SFXdisappear;

    private void OnDisable()
    {
        AudioSource.PlayClipAtPoint(SFXdisappear, Camera.main.transform.position);
    }
}
