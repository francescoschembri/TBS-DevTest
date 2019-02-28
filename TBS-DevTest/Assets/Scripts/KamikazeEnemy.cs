using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    [SerializeField] Boundary boundary;
    [SerializeField] float tilt;
    [SerializeField] float smoothing;
    [SerializeField] float xDrift, zDrift;
    [SerializeField] float driftingChance;
    [SerializeField] float rayLength;
    //[SerializeField] float minXSpeed, maxXSpeed;
    //[SerializeField] float minZSpeed, maxZSpeed;
    [SerializeField] float xSpeed, zSpeed;

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
            Vector3 direction = FindFreeDirection();

            if (Mathf.Abs(direction.z) < zSpeed)
            {
                direction.z = zSpeed * Mathf.Sign(direction.z);
            }
            if (Mathf.Abs(direction.x) < xSpeed)
            {
                direction.x = xSpeed * Mathf.Sign(direction.x);
            }

            float newXManeuver = Mathf.MoveTowards(
                                                    GetComponent<Rigidbody>().velocity.x,
                                                    direction.x,
                                                    smoothing * Time.deltaTime
                                                  );
            //float xSpeed = Mathf.Clamp(newXManeuver, minXSpeed, maxXSpeed);
            float newZManeuver = Mathf.MoveTowards(
                                                    GetComponent<Rigidbody>().velocity.z,
                                                    direction.z,
                                                    smoothing * Time.deltaTime
                                                  );
            //float zSpeed = Mathf.Clamp(newZManeuver, minZSpeed, maxZSpeed);
            //GetComponent<Rigidbody>().velocity = new Vector3(xSpeed, 0.0f, zSpeed);
            //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, xSpeed * -tilt);
            GetComponent<Rigidbody>().velocity = new Vector3(newXManeuver, 0.0f, newZManeuver);
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, newXManeuver * -tilt);
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

    Vector3 FindFreeDirection()
    {
        Vector3 targetDirection = targetTransform.position - transform.position;
        float angle = Vector3.Angle(Vector3.forward, targetDirection.normalized);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDirection, out hit, rayLength, LayerMask.GetMask("Enemy")))
        {
            if (hit.transform.Equals(targetTransform) || driftingChance < Random.Range(0f, 100f)) 
                return targetDirection;
            Vector3 zDriftingAmount = Vector3.forward * zDrift * Mathf.Sign(targetDirection.z);
            Vector3 xDriftingAmount = Vector3.right * xDrift * Mathf.Sign(targetDirection.x);
            targetDirection += zDriftingAmount;
            if (Physics.Raycast(transform.position, targetDirection, rayLength, LayerMask.GetMask("Enemy")))
            {
                targetDirection -= zDriftingAmount * 2;
            }
        }
        return targetDirection;
    }
}
