using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDrop : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public int chance = 15;

    private void OnDestroy()
    {
        int number = Random.Range(1, 101);
        if (number <= chance)
        { 
            Instantiate(powerUpPrefabs[number % powerUpPrefabs.Length], transform.position, Quaternion.identity);
        }
    }
}
