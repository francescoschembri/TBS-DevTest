using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    [SerializeField] Boundary boundary;
    [SerializeField] float tilt;
    [SerializeField] float smoothing;

    private Transform targetTransform;

    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if(player)
            targetTransform = player.transform;
    }

    void FixedUpdate ()
    {
        if (targetTransform)
        {
            float newXManeuver = Mathf.MoveTowards(
                                                    GetComponent<Rigidbody>().velocity.x,
                                                    targetTransform.position.x - transform.position.x,
                                                    smoothing * Time.deltaTime
                                                  );
            float newZManeuver = Mathf.MoveTowards(
                                                    GetComponent<Rigidbody>().velocity.z,
                                                    targetTransform.position.z - transform.position.z,
                                                    smoothing * Time.deltaTime
                                                  );
            GetComponent<Rigidbody>().velocity = new Vector3(newXManeuver, 0.0f, newZManeuver);
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -5f);
        }

        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );
    }
}
