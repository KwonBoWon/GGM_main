using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList  = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    //�浹���� �迭
    Vector2[] timingBoxs = null;

    S_EffectManager theEffect;

    void Start()
    {
        theEffect = FindObjectOfType<S_EffectManager>();

        //Ÿ�̹� �ڽ� ����

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                                        Center.localPosition.x + timingRect[i].rect.width / 2);
            //Ÿ�ֹ̹ڽ��� �浹���� perfect~bad��

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
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    boxNoteList[i].GetComponent<S_Note>().HideNote(); //�̹�������
                    if (x < timingBoxs.Length - 1)
                    {
                        theEffect.NoteHitEffect();
                    }

                    boxNoteList.RemoveAt(i);  //�迭���� ����
                    theEffect.JudgementEffect(x);
                    return;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);
    }

}
