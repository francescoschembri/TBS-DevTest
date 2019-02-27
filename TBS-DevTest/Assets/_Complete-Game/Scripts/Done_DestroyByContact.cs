using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public int scoreValue;
    public int hitsBeforeDestroy = 1;

	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == gameObject.tag)
        {
            return;
        }

        hitsBeforeDestroy--;
        if (hitsBeforeDestroy > 0)
            return;

        if (explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);

        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }
}