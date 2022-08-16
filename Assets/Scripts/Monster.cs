﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    [Header("효과음 공격 피격 사망")]
    [SerializeField]
    public Sound[] Sounds;


    [Header("몬스터 공격/방어패턴")]
    public int redTurn;
    public int blueTurn;

    [Header("몬스터 공격력/체력")]
    public int monsterDamage;
    public int monsterHP;

    [Header("보스몬스터")]
    public bool semiBoss= false;
    public bool Boss =false;

    [Header("보스 패턴")]
    public bool multi = false;
    public bool drop = false;
    public bool change = false;
    public bool doubleNote = false;

    private void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source = gameObject.AddComponent<AudioSource>();
            Sounds[i].source.volume = Sounds[i].volume;
            Sounds[i].source.clip = Sounds[i].clip;
            Sounds[i].source.loop = false;
        }
    }


    public void PositionReset()
    {
        this.transform.position = new Vector3(1000, 596, 0);
    }




}
