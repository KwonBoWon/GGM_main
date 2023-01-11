using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class S_TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList  = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    //�浹���� �迭
    Vector2[] timingBoxs = null;

    S_EffectManager theEffect;

    float[] sync_arry = new float[11];//싱크 보정위한 배열선언
    int sync_cnt = 0;
    public static float sync_value = 0;

    void Start()
    {
        theEffect = FindObjectOfType<S_EffectManager>();
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
        for (int i = 0; i < boxNoteList.Count; i++)//현재노트들
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;


            for (int x= 0; x < timingBoxs.Length; x++)//판정범위들
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    
                    boxNoteList[i].GetComponent<S_Note>().HideNote();
                    
                    
                    Debug.Log(boxNoteList[i].transform.localPosition.x);
                    if(sync_cnt<10){
                        sync_arry[sync_cnt] = boxNoteList[i].transform.localPosition.x;//싱크배열에추가
                        sync_cnt++;
                    }
                    else{//10번 체크하면
                        for(int s=0;s<10;s++){
                            sync_value+= sync_arry[s];
                        }
                        sync_value /= 10;//싱크평균값구함
                        
                        GoMain();
                    }
                    if (x < timingBoxs.Length - 1)
                    {
                        theEffect.NoteHitEffect();
                    }

                    boxNoteList.RemoveAt(i);
                    //theEffect.JudgementEffect(x);
                    return;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);//모두확인했는데 아니면 miss
    }

    public void GoMain(){
        Time.timeScale = 1.0F;
        SceneManager.LoadScene("Start");
    }
}
