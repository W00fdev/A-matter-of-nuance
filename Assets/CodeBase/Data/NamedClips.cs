using UnityEngine;

[CreateAssetMenu(fileName = "New Clip", menuName = "Sample Data", order = 53)]
public class NamedClips : ScriptableObject
{
    public AudioClip Clip;
    public string Name;
    public float volume;
}
