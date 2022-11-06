using UnityEngine;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    public TextMeshProUGUI TextReligion;
    public TextMeshProUGUI TextArmy;
    public TextMeshProUGUI TextFood;

    private void Awake()
    {
        TextReligion.text = "";
        TextArmy.text = "";
        TextFood.text = "";
    }

    public void UpdateReligion(float newValue)
        => TextReligion.text = newValue.ToString();

    public void UpdateFood(float newValue) 
        => TextFood.text = newValue.ToString();

    public void UpdateArmy(float newValue)
        => TextArmy.text = newValue.ToString();
}
