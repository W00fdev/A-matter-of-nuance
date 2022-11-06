using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public List<NamedClips> ClipSamples;

    private AudioSource _audioSource;

    [Header("Read-only clipNames")]
    [SerializeField] private string ScrollUp = "ScrollUp";
    [SerializeField] private string ScrollDown = "ScrollDown";


    private void Start()
    {
        if (Instance == null)
        { 
            Instance = this; 
        }
        else if (Instance == this)
        { 
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }

    private void InitializeManager()
    {
        _audioSource = GetComponent<AudioSource>();

        //Resources.LoadAll<ScrollData>("Scrolls");
    }

    private AudioClip ExtractClipByName(string name)
    {
        foreach (NamedClips namedClip in ClipSamples)
            if (namedClip.Name == name)
                return namedClip.Clip;

        throw new UnityException("No such clip: " + name);
    }

    public void PlayScrollUp()
        => _audioSource.PlayOneShot(ExtractClipByName(ScrollUp));
    
    public void PlayScrollDown()
        => _audioSource.PlayOneShot(ExtractClipByName(ScrollDown));
}

[Serializable]
public class NamedClips
{
    public AudioClip Clip;
    public string Name;
}
