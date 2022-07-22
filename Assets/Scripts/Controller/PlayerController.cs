using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    public Animator animator; //애니메이터 변수 선언
    public Slider MonsterHP;
    private float maxHP = 100; //최대 체력
    private float curHP = 100; //현재 체력
    void Start()
    {
        MonsterHP.value = (float) curHP / (float) maxHP;
        theTimingManager = FindObjectOfType<TimingManager>();//������Ʈ�� ã��(TimingManager)
    }
    void Update()
    {
        //화살표 입
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            theTimingManager.CheckTiming(0); 
            curHP -= 10; //현재 체력 깎는 걸 판정 코드에 넣는 게 더 좋을 듯
            animator.SetBool("Down", true); 
            Debug.Log("down");
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            theTimingManager.CheckTiming(1);
            curHP -= 10;
            animator.SetBool("Up", true);
            Debug.Log("up");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            theTimingManager.CheckTiming(2);
            curHP -= 10;
            animator.SetBool("Left", true);
            Debug.Log("left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            theTimingManager.CheckTiming(3);
            curHP -= 10;
            animator.SetBool("Right", true);
            Debug.Log("right");
        }
        HandleHP();
       Stop();
    }
    private void HandleHP() {
        MonsterHP.value = (float) curHP / (float) maxHP;
    }
    private void Stop() {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Down") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetBool("Down", false);
            }

        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Up") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetBool("Up", false);
            }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Left") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetBool("Left", false);
            }
        
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Right") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetBool("Right", false);
            }
    }
   
}
