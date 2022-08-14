using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    ComboManager thecomboManager;
    [SerializeField] Animator[] player;
    [SerializeField]  Animator[] playerEffect;
    public static int n = 0; //플레이어가 무슨 무기를 들고 있는지 나타내는 변수 0: 칼, 1: 방망이, 2: 지팡이
    public Animator animator; //애니메이터 변수 선언
    public List<GameObject> boxNoteList  = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null; //충돌범위 배열
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    GameObject Obj;
    public string[] arrows;

    public static bool immortal = false; //무적
    void Start()
    {
        thecomboManager = FindObjectOfType<ComboManager>();

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
        if (arrowInput == -10 && !immortal ) //노트 놓칠시
        {
            PlayerController.curTime -= 10; //시간감소
            theEffect.JudgementEffect(4); //Miss이펙트
            thecomboManager.ResetCombo(); //콤보리셋
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
                        thecomboManager.IncreaseCombo();//콤보증가
                    }
                    else if (arrowInput != t_noteType && t_noteType == 0 && !immortal)
                    {//방어실패
                        PlayerController.curTime -= PlayerController.nowMonster.GetComponent<Monster>().monsterDamage; //시간감소 몬스터공격력만큼
                        theEffect.JudgementEffect(4); //Miss이펙트
                        thecomboManager.ResetCombo(); //콤보리셋
                        player[n].SetTrigger("Hitted"); //플레이어 피격 모션
                    }

                    if ( t_noteType == 1) //반대방향으로 누를때
                    {//공격성공
                        if (arrowInput + t_noteDire == 1 || arrowInput + t_noteDire == 5)
                        {
                            if (x < timingBoxs.Length - 1)
                            {
                                theEffect.NoteHitEffect();//히트이펙트
                            }
                            theEffect.JudgementEffect(x); //판정 이펙트
                            thecomboManager.IncreaseCombo();//콤보증가
                            Obj.GetComponent<PlayerController>().curHP -= PlayerController.playerDamage;
                            player[n].SetTrigger("hit"); //플레이어 공격 모션
                            playerEffect[n].SetTrigger("hit"); //플레이어 공격 이펙트
                        }
                        else// 공격실패
                        {
                            if ( !immortal) {
                                theEffect.JudgementEffect(4); //Miss이펙트
                                thecomboManager.ResetCombo(); //콤보리셋
                            }
                        }
                    }
                    //Debug.Log(arrows[arrowInput]);
                    boxNoteList[i].GetComponent<Note>().HideNote(); //이미지삭제
                    boxNoteList.RemoveAt(i);  //배열에서 삭제               
                    return;
                }

            }
        }
        theEffect.JudgementEffect(timingBoxs.Length);
    }

}
