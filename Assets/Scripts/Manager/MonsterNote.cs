using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNote : MonoBehaviour
{
    TimingManager theTimingManager;
    MonsterManager theMonsterManager;
    Animator monsterAttack = null;
    [SerializeField]  Animator player;
    [SerializeField]  Animator Final2;
    [SerializeField]  Animator Sub1;
    [SerializeField]  Animator Sub4;
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
                    PlayerController.nowMonster.GetComponent<Monster>().Sounds[0].source.Play();
                    if (PlayerController.pStage == 3) {
                        if (PlayerController.nStage == 2) {
                            Final2.SetTrigger("hit");
                        }
                    }
                    else if (PlayerController.nStage == 1) {
                        Sub1.SetTrigger("hit");
                    }
                    else if (PlayerController.nStage == 4) {
                        Debug.Log("dkfd");
                        Sub4.SetTrigger("hit");
                    }
                    
                }
                if (collision.GetComponent<Note>().noteType == 1) //회피
                {
                    /*
                    if (dir == 0) monsterAttack.SetTrigger("Down");
                    else if (dir == 1) monsterAttack.SetTrigger("Up");
                    else if (dir==2) monsterAttack.SetTrigger("Left");
                    else if (dir == 3) monsterAttack.SetTrigger("Right");
                    */
                }
            }
        }
    }

    void Update()
    {
        
    }
}
