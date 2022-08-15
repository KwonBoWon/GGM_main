﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BGM
{
    public string soundName;
    public AudioClip clip;
    public AudioSource source;
    public int bpm;
}
public class CenterFlame : MonoBehaviour
{
    public static CenterFlame instance;
    TimingManager theTimingManager;
    [Header("배경음악")]
    [SerializeField]
    public BGM[] bgms;

    //public AudioSource myAudio;
    bool musicStart = false;


    private void Start()
    {
        instance = this;
        theTimingManager = FindObjectOfType<TimingManager>();

        for (int i = 0; i < bgms.Length; i++)
        {

            bgms[i].source = gameObject.AddComponent<AudioSource>();
            bgms[i].source.clip = bgms[i].clip;
            bgms[i].source.loop = false;
        }

        //myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 노트 충돌시 음악 시작
        if(!musicStart){
            if (collision.CompareTag("Note"))
            {
                bgms[PlayerController.nStage].source.Play();
                musicStart = true;
            }
        }
    }

    public void StopMusic()
    {
        bgms[PlayerController.nStage].source.Stop();
        musicStart = false;
        NoteClear();
    }
    public void NoteClear()
    {
        GameObject[] destroyNotes = null;
        if (destroyNotes == null)
        {
            destroyNotes = GameObject.FindGameObjectsWithTag("Note");
        }
        foreach (GameObject note in destroyNotes)
        {
            Destroy(note);
        }
         destroyNotes = null;
         theTimingManager.boxNoteList.Clear();
    }

    void Update()
    {
        
    }
}
