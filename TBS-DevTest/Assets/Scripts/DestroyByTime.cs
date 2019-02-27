using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] float lifetime;

	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}
