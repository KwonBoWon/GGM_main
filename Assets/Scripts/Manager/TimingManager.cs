using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList  = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    //충돌범위 배열
    Vector2[] timingBoxs = null;

    EffectManager theEffect;

    void Start()
    {
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

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x= 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)//범위안에 들어왔을때
                {
                    //체력닳는것

                    boxNoteList[i].GetComponent<Note>().HideNote(); //이미지삭제
                    if (x < timingBoxs.Length - 1)
                    {
                        theEffect.NoteHitEffect();//이펙트
                    }

                    boxNoteList.RemoveAt(i);  //배열에서 삭제
                    theEffect.JudgementEffect(x);
                    return;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);
    }

}
