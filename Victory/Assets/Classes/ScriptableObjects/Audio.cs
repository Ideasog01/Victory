using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Audio", order = 0)]
public class Audio : ScriptableObject
{
    public AudioClip audioClip;

    public float volume;

    public bool isMusic;

    public string audioName;
}