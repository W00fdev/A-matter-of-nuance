using UnityEngine;

public class PlayStampSound : MonoBehaviour
{
    public AudioSource _source;
    public AudioClip clip;
    public AudioClip clip2;

    public void PlayStamp() =>
        _source.PlayOneShot(clip);
    public void FallStamp() =>
        _source.PlayOneShot(clip2);
}
