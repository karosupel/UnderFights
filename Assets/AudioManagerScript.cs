using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript Instance;

    public AudioSource audioSource;

    public Dictionary<string,AudioClip> soundEffects;
    public AudioClip click;
    public AudioClip death;
    public AudioClip hit;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffects = new Dictionary<string, AudioClip>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        soundEffects.Add("click", click);
        soundEffects.Add("death", death);
        soundEffects.Add("hit", hit);
    }

    void Update()
    {

    }

    public void PlaySFX(string name)
    {
        AudioClip clip = soundEffects[name];
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
