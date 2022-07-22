﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public Animator animator; //애니메이터 변수 선언
    public List<GameObject> boxNoteList  = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    //충돌범위 배열
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    GameObject Obj;
    public string[] arrows;
    void Start()
    {
        Obj = GameObject.Find("PlayerControll");//게임오브젝트를 찾음
        

        theEffect = FindObjectOfType<EffectManager>();

        //타이밍 박스 설정

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                                        Center.localPosition.x + timingRect[i].rect.width / 2);
            //타이밍박스의 충돌판정 perfect~bad순

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckTiming( int arrowInput)
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x; //노트 x좌표 가져옴
            int t_noteDire = boxNoteList[i].GetComponent<Note>().noteDirection; // 노트방향값 가져옴

            for (int x= 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y && arrowInput == t_noteDire) //범위안에 들어오고 방향키 방향이 맞을때
                {
                    //체력닳는것
                    Obj.GetComponent<PlayerController>().curHP -= 10;
                    if (arrowInput == 0){
                        animator.SetBool("Down", true);
                    }
                    else if (arrowInput == 1) {
                        animator.SetBool("Up", true);
                    }
                    else if (arrowInput == 2) {
                        animator.SetBool("Left", true);
                    }
                    else {
                        animator.SetBool("Right", true);
                    }
                    //Debug.Log(arrows[arrowInput]);
                    boxNoteList[i].GetComponent<Note>().HideNote(); //이미지삭제
                    if (x < timingBoxs.Length - 1)
                    {
                        theEffect.NoteHitEffect();//이펙트
                    }

                    boxNoteList.RemoveAt(i);  //배열에서 삭제
                    theEffect.JudgementEffect(x);
                    Stop();
                    return;
                }

            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);
    }
    public void Stop() {
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
