using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float tilt;
    [SerializeField] int health = 3;
    [SerializeField] Boundary boundary;
    [SerializeField] GameObject explosionVFX;

    [SerializeField] HomingMissile missilePrefab;
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
    [SerializeField] float fireRate;

    private GameController gameController;
    private float nextFire;
    private GameObject shield;
    private float remainingTimeSecondWeapon = 0;
    
    public int GetHealth() { return health; }

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
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

        if (health == 1)
            gameController.DangerAlertOn();

        if (health == 0)
        {
            gameController.DangerAlertOff();
            if (explosionVFX != null)
            {
                Instantiate(explosionVFX, transform.position, transform.rotation);
            }
            Instantiate(explosionVFX, transform.position, transform.rotation);
            gameController.GameOver();
            Destroy(gameObject);
        }
    }

    //power up
    public void AddLife()
    {
        if (health < 3)
            health++;
        if (health == 2)
        {
            gameController.DangerAlertOff();
        }
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
