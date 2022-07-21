using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;
    int arrowDirection =0;

    [SerializeField] Transform tfNoteAppear  = null; //노트가 생성되는곳
    [SerializeField] GameObject[] goNote = null; //노트 프리펩들

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        //오브젝트에서 컴포넌트를 가져옴(여기선 TimingManager 스크립트)
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 110d / bpm)  //55bpm마다 노트 생성
        {
            arrowDirection = Random.Range(0, 4); // 무작위로 방향 지정
            GameObject t_note = Instantiate(goNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // 노트를 생성
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가
            currentTime -= 110d / bpm; //-하지않고 0으로설정하면 시차가 생김
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Note")) 
        {
         
            /*
            if (collision.GetComponent<Note>().GetNoteFlag//이미지가 있을때만
            {
                theEffectManager.JudgementEffect(4);//노트 놓쳤을때 MISS
            }
          */

            //노트가 맵 끝까지 가면 삭제
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }



}
