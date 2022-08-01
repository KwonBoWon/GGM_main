using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public Animator animator; //애니메이터 변수 선언
    public List<GameObject> boxNoteList  = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null; //충돌범위 배열
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    GameObject Obj;
    public string[] arrows;
    void Start()
    {
        Obj = GameObject.Find("PlayerControll");
        theEffect = FindObjectOfType<EffectManager>();
        timingBoxs = new Vector2[timingRect.Length];//타이밍 박스 설정

        for (int i = 0; i < timingRect.Length; i++)
        { //타이밍박스의 충돌판정 perfect~bad순
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                                        Center.localPosition.x + timingRect[i].rect.width / 2);
            

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckTiming( int arrowInput)
    {
        if (arrowInput == -1) //노트 놓칠시
        {
            Obj.GetComponent<PlayerController>().curTime -= 10; //시간감소
            theEffect.JudgementEffect(4); //Miss이펙트
            return;
        }

        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x; //노트 x좌표 가져옴
            int t_noteDire = boxNoteList[i].GetComponent<Note>().noteDirection; // 노트방향값 가져옴
            int t_noteType = boxNoteList[i].GetComponent<Note>().noteType; //노트타입 가져옴 0:방어턴 1:공격턴

            for (int x= 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {//범위, 방향키 확인
                    if (arrowInput == t_noteDire && t_noteType == 0)
                    {//방어성공
                        if (x < timingBoxs.Length - 1)
                        {
                            theEffect.NoteHitEffect();//히트이펙트
                        }
                        theEffect.JudgementEffect(x); //판정 이펙트
                    }
                    else if (arrowInput != t_noteType && t_noteType == 0)
                    {//방어실패
                        Obj.GetComponent<PlayerController>().curTime -= 10; //시간감소
                        theEffect.JudgementEffect(4); //Miss이펙트
                    }

                    if ((arrowInput + t_noteDire ==1  || arrowInput + t_noteDire == 5) && t_noteType == 1) //반대방향으로 누를때
                    {//공격성공

                        if (x < timingBoxs.Length - 1)
                        {
                            theEffect.NoteHitEffect();//히트이펙트
                        }
                        theEffect.JudgementEffect(x); //판정 이펙트
                        Obj.GetComponent<PlayerController>().curHP -= 10;
                        if (arrowInput == 0)
                        {
                            animator.SetBool("Down", true);
                        }
                        else if (arrowInput == 1)
                        {
                            animator.SetBool("Up", true);
                        }
                        else if (arrowInput == 2)
                        {
                            animator.SetBool("Left", true);
                        }
                        else
                        {
                            animator.SetBool("Right", true);
                        }
                    }
                    
                    //Debug.Log(arrows[arrowInput]);
                    boxNoteList[i].GetComponent<Note>().HideNote(); //이미지삭제
                   
                    boxNoteList.RemoveAt(i);  //배열에서 삭제

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
