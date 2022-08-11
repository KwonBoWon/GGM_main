using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    [Header("���� ����/�������")]
    public int monsterAttack;
    public int monsterDefense;

    [Header("���� ���ݷ�/ü��")]
    public int monsterDamage;
    public int monsterHP;

    [Header("��������")]
    public bool semiBoss= false;
    public bool Boss =false;
    [Header("���� ����[0]=��Ÿ")]
    public bool multi = false;
    public bool drop = false;


    public void MonsterDoge( int direction)
    {
        
        float speed = 5000;//�Ʒ����޿�
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
