using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
	[SerializeField] float fireRate;
    [SerializeField] float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		GetComponent<AudioSource>().Play();
	}
}
