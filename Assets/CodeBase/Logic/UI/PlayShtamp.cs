using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShtamp : MonoBehaviour
{
    public AudioSource _source;
    public AudioClip clip;

    public void PlayStamp() =>
        _source.PlayOneShot(clip);
}
