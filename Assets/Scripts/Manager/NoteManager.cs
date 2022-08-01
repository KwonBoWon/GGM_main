using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    
    private int bpm = 0;
    double currentTime = 0d;
    int arrowDirection =0;
    public static bool noteOn = true;
    private int noteCount=0;

    [SerializeField] Transform tfNoteAppear  = null; //노트가 생성되는곳
    [SerializeField] GameObject[] goNote = null; //노트 프리펩들

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        if (PlayerController.cnt % 3 != 0 || PlayerController.cnt==0)
        {
            if ((noteCount % 10) < 5) MakeNode(0);
            else MakeNode(1);
            //Debug.Log(noteCount);
        }
        else CenterFlame.instance.StopMusic();
    }

    public int MakeNode(int turn)//(int attackTurn , int defenseTurn)
    {
        bpm = CenterFlame.instance.bgms[BackGroundManager.stage].bpm;
        currentTime += Time.deltaTime;
        if (noteOn == true)
        {
            if (currentTime >= 60d / bpm)  //55bpm마다 노트 생성
            {
                if(turn == 0) arrowDirection = Random.Range(0, 4); // 방어턴
                else if(turn ==1)  arrowDirection = Random.Range(4, 8); //공격턴

                GameObject t_note = Instantiate(goNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // 노트를 생성
                t_note.transform.SetParent(this.transform);
                theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가
                currentTime -= 60d / bpm; //-하지않고 0으로설정하면 시차가 생김
                noteCount++; //noteCount증가
            }
        }

        return noteCount;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Note")) 
        {
            
            if (collision.GetComponent<Note>().GetNoteFlag() && collision.GetComponent<Note>().noteType==0)//이미지가 있을때만 방어턴일때만
            {
                theTimingManager.CheckTiming(-1); //노트 놓칠시
            }
          
            //노트가 맵 끝까지 가면 삭제
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }



}
