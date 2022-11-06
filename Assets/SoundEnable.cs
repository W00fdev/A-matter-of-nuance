using UnityEngine;

public class SoundEnable : MonoBehaviour
{
    public GameObject sound;

    public void Enable() => sound.SetActive(true);

    private void OnDisable()
    {
        Enable();
    }
}
