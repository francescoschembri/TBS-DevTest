using UnityEngine;
using UnityEngine.UI;

public class DangerVFX : MonoBehaviour
{
    [SerializeField] float maxAlpha;
    [SerializeField] float minAlpha;
    [SerializeField] float alphaPerSecond;

    private Image effect;
    private bool increase = true;
    private float currentAlpha;

    private void Start()
    {
        currentAlpha = minAlpha;
        effect = GetComponent<Image>();
    }

    void Update()
    {
        Color temp = effect.color;
        float amount = alphaPerSecond * Time.deltaTime;
        if (increase)
        {
            temp.a += amount;
            effect.color = temp;
            currentAlpha += amount;
            if (currentAlpha >= maxAlpha)
                increase = false;
        }
        else
        {
            temp.a -= amount;
            effect.color = temp;
            currentAlpha -= amount;
            if(currentAlpha <= minAlpha)
                increase = true;
        }
    }

    private void OnDisable()
    {
        effect.color = new Color(1, 0, 0, minAlpha);
        currentAlpha = minAlpha;
        increase = true;
    }
}
