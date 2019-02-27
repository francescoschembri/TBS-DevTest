using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] AudioClip SFXDisappear;

    private void OnDisable()
    {
        if(SFXDisappear)
            AudioSource.PlayClipAtPoint(SFXDisappear,
                                        Camera.main.transform.position,
                                        FindObjectOfType<GameController>().GetSFXMultiplier());
    }
}
