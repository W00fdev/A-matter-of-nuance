using UnityEngine.UI;
using UnityEngine;

public class TimeManagerUI : MonoBehaviour
{
    public Image FrozenImage;

    private void Start()
    {
        FrozenImage.fillAmount = 0f;
    }

    public void ChangeFrozen(float coeff)
        => FrozenImage.fillAmount = coeff;
}
