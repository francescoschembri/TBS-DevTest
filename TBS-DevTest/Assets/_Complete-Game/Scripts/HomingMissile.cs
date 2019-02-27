using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public AudioClip SFXshot;
    public float speed = 10f;
    public float rotateSpeed = 10f;

    private GameObject target;

    private void Start()
    {
        if(SFXshot)
            AudioSource.PlayClipAtPoint(SFXshot, Camera.main.transform.position);

        //select target
        float distance = 0;
        Done_WeaponController[] enemies = GameObject.FindObjectsOfType<Done_WeaponController>();
        if (enemies.Length > 0) {
            target = enemies[0].gameObject;
            distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        }
        foreach (Done_WeaponController enemy in enemies)
        {
            float newDistance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                target = enemy.gameObject;
            }
        }
    }

    private void Update()
    {
        if (target)
        {
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
            Destroy(gameObject);
    }

}
