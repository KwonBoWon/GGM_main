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

    void Start()
    {
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
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    //이미지삭제
                    boxNoteList.RemoveAt(i);
                    //배열에서 삭제
                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }

        Debug.Log("Miss");
        
    }

}
