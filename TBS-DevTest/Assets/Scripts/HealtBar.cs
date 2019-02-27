using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    private PlayerController player;
    private Image fillArea;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        fillArea = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }
    
    void Update()
    {
        int health = 0;
        if (player)
        {
            health = player.GetHealth();
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
        GetComponent<Slider>().value = health;
    }
}
