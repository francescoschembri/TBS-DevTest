using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
    public int health = 3;
	public Done_Boundary boundary;
    public GameObject explosion;

    public HomingMissile missilePrefab;
    public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

    private Done_GameController gameController;
    private float nextFire;
    private GameObject shield;
    private float remainingTimeSecondWeapon = 0;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        shield = transform.Find("Shield").gameObject;
    }

    void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Power Up" || other.tag == "Player shoot")
        {
            return;
        }

        if (shield.activeSelf)
            shield.SetActive(false);
        else
            health--;
        if (health == 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.GameOver();
        }
    }

    public void AddLife()
    {
        if (health < 3)
            health++;
    }

    public void ActiveShield()
    {
        shield.SetActive(true);
    }

    public void ActiveSecondWeapon()
    {
        if (remainingTimeSecondWeapon < Mathf.Epsilon)
        {
            remainingTimeSecondWeapon += 20f;
            StartCoroutine(SecondWeapon());
        }
        else
        {
            remainingTimeSecondWeapon += 20f;
        }
    }

    IEnumerator SecondWeapon() {
        while (remainingTimeSecondWeapon > Mathf.Epsilon)
        {
            Instantiate(missilePrefab, shotSpawn.position, Quaternion.identity);
            remainingTimeSecondWeapon -= 5f;
            yield return new WaitForSeconds(5f);
        }
    }

    public int GetHealth() { return health; }
}
