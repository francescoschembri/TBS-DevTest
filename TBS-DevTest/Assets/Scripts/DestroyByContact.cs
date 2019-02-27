using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] int scoreValue;
    [SerializeField] int hitsBeforeDestroy = 1;

	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
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

        if (explosionVFX != null)
            Instantiate(explosionVFX, transform.position, transform.rotation);

        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }
}