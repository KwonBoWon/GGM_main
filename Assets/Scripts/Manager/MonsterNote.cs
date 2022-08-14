using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNote : MonoBehaviour
{
    TimingManager theTimingManager;
    MonsterManager theMonsterManager;
    Animator monsterAttack = null;
    [SerializeField]  Animator player;
    GameObject t_monster;
    int dir;
    
    void Start()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            t_monster = PlayerController.nowMonster;
            monsterAttack = t_monster.GetComponent<Animator>();
            dir = collision.GetComponent<Note>().noteDirection;
            if (t_monster != null)
            {
                t_monster.GetComponent<Monster>().PositionReset();
                
                if (collision.GetComponent<Note>().noteType == 0) //공격
                {
                    monsterAttack.SetTrigger("hit");
                    if (dir == 0)
                    {
                        player.SetTrigger("Down");
                    }
                    else if (dir == 1)
                    {
                        player.SetTrigger("Up");
                    }
                    else if (dir == 2)
                    {
                        player.SetTrigger("Left");
                    }
                    else if(dir==3)
                    {
                        player.SetTrigger("Right");
                    }
                }
                if (collision.GetComponent<Note>().noteType == 1) //회피
                {
                    if (dir == 0) monsterAttack.SetTrigger("Down");
                    else if (dir == 1) monsterAttack.SetTrigger("Up");
                    else if (dir==2) monsterAttack.SetTrigger("Left");
                    else if (dir == 3) monsterAttack.SetTrigger("Right");
                }
            }
        }
    }

    void Update()
    {
        
    }
}
