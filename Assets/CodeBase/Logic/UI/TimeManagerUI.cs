using UnityEngine.UI;
using UnityEngine;

public class TimeManagerUI : MonoBehaviour
{
    public Image FrozenImage;
    public Image ProgressImage;

    private void Start()
    {
        FrozenImage.fillAmount = 0f;
        ProgressImage.fillAmount = 0f;
    }

    public void ChangeFrozen(float coeff) 
        => FrozenImage.fillAmount = coeff;

    public void ChangeProgress(float coeff) 
        => ProgressImage.fillAmount = coeff;

}
