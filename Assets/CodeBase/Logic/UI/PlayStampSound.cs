using UnityEngine;

public class PlayStampSound : MonoBehaviour
{
    public AudioSource _source;
    public AudioClip clip;

    public void PlayStamp() =>
        _source.PlayOneShot(clip);
}
