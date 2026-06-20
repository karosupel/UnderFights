using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public static MusicManagerScript Instance;
    private AudioSource audioSource;
    public List<AudioClip> music;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBackgroundMusic(false, 0);
    }

    public void PlayBackgroundMusic(bool resetSong, int SceneIndex)
    {
        AudioClip clip = music[SceneIndex];
        if(clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else if (audioSource.clip != null)
        {
            if(resetSong)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
}
