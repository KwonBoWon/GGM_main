using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNote : MonoBehaviour
{
    TimingManager theTimingManager;
    MonsterManager theMonsterManager;
    Animator monsterAttack = null;
    GameObject t_monster;
    int dir;
    void Start()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            t_monster = GameObject.Find("monster" + PlayerController.cnt + "(Clone)");
            monsterAttack = t_monster.GetComponent<Animator>();
            if (t_monster != null)
            {
                t_monster.GetComponent<Monster>().PositionRest();
                if (collision.GetComponent<Note>().noteType == 0) //공격
                {
                    monsterAttack.SetTrigger("hit");
                }
                if (collision.GetComponent<Note>().noteType == 1) //회피
                {


                    //Debug.Log(t_monster);
                    dir = collision.GetComponent<Note>().noteDirection;
                    t_monster.GetComponent<Monster>().MonsterDoge(dir);


                }
            }
        }
    }

    void Update()
    {
        
    }
}
