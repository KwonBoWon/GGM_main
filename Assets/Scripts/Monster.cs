using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    [Header("효과음 공격 피격 사망")]
    [SerializeField]
    public Sound[] Sounds;


    [Header("몬스터 공격/방어패턴")]
    public int monsterAttack;
    public int monsterDefense;

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


    public void MonsterDoge( int direction)
    {
        
        float speed = 5000;//아래위왼오
        //this.transform.position = new Vector3(999, 595, 0);
        if (direction==0) this.transform.Translate(0, -(speed * Time.deltaTime),  0);
        else if (direction == 1) this.transform.Translate(0, speed * Time.deltaTime,  0);
        else if (direction == 2) this.transform.Translate(-(speed* Time.deltaTime), 0, 0);
        else if (direction == 3) this.transform.Translate(speed *Time.deltaTime, 0, 0);
        //Debug.Log("moved");
       
    }
    public void PositionReset()
    {
        this.transform.position = new Vector3(1000, 596, 0);
    }
    public void OnIvoke()
    {
        return;
    }




}
