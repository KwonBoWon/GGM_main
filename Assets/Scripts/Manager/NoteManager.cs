using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{


    private int bpm = 0;
    double currentTime = 0d;
    int arrowDirection = 0;
    public static bool noteOn = true;
    public static int noteCount = 0;

    bool attackPlag = true;

    int invokeCnt=0;
    int multi = 5;
    [SerializeField] Transform tfNoteAppear = null; //노트가 생성되는곳
    [SerializeField] GameObject[] goNote = null; //노트 프리펩들

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager thecomboManager;


    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        thecomboManager = FindObjectOfType<ComboManager>();
    }

    void Update()
    {
        if (PlayerController.flag != 3)
        {
            if (noteCount % 10 == 0 && noteCount != 0 && attackPlag)
            {
                Multistrike();
            }
            else if ((noteCount % 10) < 5) MakeNode(0);
            else if ((noteCount % 10) >= 5)MakeNode(1);
        }
        else CenterFlame.instance.StopMusic();

        if (invokeCnt >= multi)
        {
            CancelInvoke(nameof(SpawnNote));
            invokeCnt = 0;
            Invoke(nameof(NoteOn), 60f / bpm);
            currentTime = 0;
        }

    }


    public void SpawnNote()
    {
        GameObject t_note = Instantiate(goNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // 노트를 생성
        t_note.transform.SetParent(this.transform);
        theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가
        invokeCnt++;

    }

    public int MakeNode(int turn)//(int attackTurn , int defenseTurn)
    {

        bpm = CenterFlame.instance.bgms[PlayerController.nStage].bpm;
        currentTime += Time.deltaTime;
        if (noteOn == true)
        {
            if (currentTime >= 60d / bpm)  //55bpm마다 노트 생성
            {
                if (turn == 0) arrowDirection = Random.Range(0, 4); // 방어턴
                else if (turn == 1) arrowDirection = Random.Range(4, 8); //공격턴 

                GameObject t_note = Instantiate(goNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // 노트를 생성
                t_note.transform.SetParent(this.transform);
                theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가

                currentTime -= 60d / bpm; //-하지않고 0으로설정하면 시차가 생김
                noteCount++; //noteCount증가
                attackPlag = true;
            }
        }

        return noteCount;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Note"))
        {

            if (collision.GetComponent<Note>().GetNoteFlag())//이미지가 있을때만
            {
                if (collision.GetComponent<Note>().noteType == 0)//방어턴일때
                {
                    theTimingManager.CheckTiming(-10); //노트 놓칠시
                }
                thecomboManager.ResetCombo(); //콤보리셋
            }

            //노트가 맵 끝까지 가면 삭제
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);

        }
    }

    public void Multistrike()
    {
        Debug.Log("multi");
        noteOn = false;
        attackPlag = false;
        arrowDirection = Random.Range(0, 4);
        InvokeRepeating(nameof(SpawnNote), 60f / bpm, 0.1f);
    }
    void NoteOn()
    {
        noteOn= true;
    }
}
