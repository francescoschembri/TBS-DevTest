using UnityEngine;

public class RandomRotator : MonoBehaviour 
{
    [SerializeField] float tumble;
	
	void Start ()
	{
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
	}
}