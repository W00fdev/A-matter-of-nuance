using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    public Image Religion;
    public Image Army;
    public Image Food;

    private void Awake()
    {
    }

    public void UpdateReligion(float newValue)
        => Religion.fillAmount = newValue;

    public void UpdateFood(float newValue) 
        => Food.fillAmount = newValue;

    public void UpdateArmy(float newValue)
        => Army.fillAmount = newValue;
}
