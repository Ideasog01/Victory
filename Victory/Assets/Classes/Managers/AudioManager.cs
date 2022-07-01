using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSourceList = new List<AudioSource>();

    [SerializeField]
    private AudioSource audioSourceTemplate;

    [SerializeField]
    private Audio[] audioLibrary;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void PlayGlobalSound(string audioName)
    {
        AudioSource audioSource = Instantiate(audioSourceTemplate, _mainCamera.transform.position, Quaternion.identity);

        foreach(Audio audio in audioLibrary)
        {
            if(audio.audioName == audioName)
            {
                if (!audio.isMusic)
                {
                    audioSource.clip = audio.audioClip;
                    audioSource.volume = audio.volume;
                    audioSource.Play();
                }
            }
        }
    }

    public void PlayLocalSound(string audioName, Vector3 position)
    {
        AudioSource audioSource = Instantiate(audioSourceTemplate, position, Quaternion.identity);

        foreach (Audio audio in audioLibrary)
        {
            if (audio.audioName == audioName)
            {
                if (!audio.isMusic)
                {
                    audioSource.clip = audio.audioClip;
                    audioSource.volume = audio.volume;
                    audioSource.Play();
                }
            }
        }
    }
}
