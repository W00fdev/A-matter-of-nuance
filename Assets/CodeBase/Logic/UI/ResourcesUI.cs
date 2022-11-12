using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    public Image Religion;
    public Image Army;
    public Image Food;

    [Range(0f, 1f)]
    public float alertValue;
    public Color okayColor;
    public Color alertColor;

    public void UpdateReligion(float newValue) 
        => UpdateResource(Religion, newValue);

    public void UpdateFood(float newValue)
        => UpdateResource(Food, newValue);

    public void UpdateArmy(float newValue)
        => UpdateResource(Army, newValue);

    private void UpdateResource(Image image, float newValue)
    {
        image.fillAmount = newValue;
        image.color = newValue > alertValue ? okayColor : alertColor;
    }
}
