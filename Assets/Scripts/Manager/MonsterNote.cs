using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNote : MonoBehaviour
{
    TimingManager theTimingManager;
    MonsterManager theMonsterManager;
    GameObject t_monster;
    float speed = 10;
    int dir;
    void Start()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            t_monster = GameObject.Find("monster" + PlayerController.cnt + "(Clone)");
            t_monster.GetComponent<Monster>().PositionRest();
            if ( collision.GetComponent<Note>().noteType == 0) //���� ����
            {
                //���� ���� �ִϸ��̼�
            }
            if (collision.GetComponent<Note>().noteType ==1) //���� ȸ��
            {

                
                //Debug.Log(t_monster);
                dir = collision.GetComponent<Note>().noteDirection;
                t_monster.GetComponent<Monster>().MonsterDoge(dir);


            }

        }
    }

    void Update()
    {
        
    }
}
