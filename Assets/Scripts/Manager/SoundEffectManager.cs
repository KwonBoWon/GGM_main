using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Sound
{
    public AudioClip clip;
    public AudioSource source;
    public float volume;
}
public class SoundEffectManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] Sounds;


    public static SoundEffectManager instance;  
    // Start is called before the first frame update


    void Start()
    {
        instance = this;

        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source = gameObject.AddComponent<AudioSource>();
            Sounds[i].source.volume = Sounds[i].volume;
            Sounds[i].source.clip = Sounds[i].clip;
            Sounds[i].source.loop = false;
        }
        Sounds[1].source.Play();
    }
}
