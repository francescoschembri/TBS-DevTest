using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    private Done_PlayerController player;
    private Image fillArea;

    private void Start()
    {
        player = FindObjectOfType<Done_PlayerController>();
        fillArea = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            int health = player.GetHealth();
            GetComponent<Slider>().value = health;
            if (health == 3)
            {
                fillArea.color = new Color(0, 255f, 0, 255f);
            }
            else if (health == 2)
            {
                fillArea.color = new Color(255f, 255f, 0, 255f);
            }
            else
            {
                fillArea.color = new Color(255f, 0, 0, 255f);
            }
        }
        else
        {
            GetComponent<Slider>().value = 0;
        }
    }
}
