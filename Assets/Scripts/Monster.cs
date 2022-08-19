using System.Collections;
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

        //mul = multi; dro = drop; cha = change; dou = doubleNote; //초기값 저장

        if (Boss)
        {
            
        }
    }


    public void PositionReset()
    {
        this.transform.position = new Vector3(1000, 596, 0);
    }
    public void BossPatten(int ns)
    {
        int rand = 0;
        if (ns == 1)
        {
            multi = true;
            drop = true;
            change = true;

            rand =Random.Range(0, 2);
            if (rand == 0)
            {
                drop = false;
            }
            else if (rand == 1)
            {
                change = false;
            }

        }
        else if (ns == 2)
        {
            multi = true;

            change = true;
            doubleNote = true;

            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                multi = false;
            }
            else if (rand == 1)
            {
                change = false;
            }
        }
        else if (ns == 3)
        {
            multi = true;
            drop = true;

            doubleNote = true;

            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                multi= false;
            }
            else if (rand == 1)
            {
                doubleNote = false;
            }
        }
        else if (ns == 4)
        {
            multi = true;
            drop = true;
            change = true;
            doubleNote = true;
            rand = Random.Range(0, 4);
            if (rand == 0)
            {
                multi = false;
            }
            else if (rand == 1)
            {
                drop = false;
            }
            else if (rand == 2)
            {
                change = false;
            }
            else if (rand == 3)
            {
                doubleNote = false;
            }
        }

    }



}
