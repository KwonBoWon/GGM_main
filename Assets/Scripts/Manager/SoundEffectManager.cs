using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Sound
{
    public AudioClip clip;
    public float volume;
}
public class SoundEffectManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] Cake, Cart, Crystal, Diamond, Drum, Golem, Note, Piano, Spider, Tree;
    public Sound[] bat, sword, wand;


    public static SoundEffectManager instance;  
    // Start is called before the first frame update


    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
