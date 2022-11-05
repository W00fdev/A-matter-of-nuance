using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShtampPlayer : MonoBehaviour
{
    public AudioSource _source;
    public AudioClip clip;
    public void PlayStamp() =>
    _source.PlayOneShot(clip);
   
}
