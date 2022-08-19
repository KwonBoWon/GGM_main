using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private int bpm = 0;
    public static double currentTime = 0d;
    int arrowDirection = 0;
    public static bool noteOn = true;
    public static int noteCount = 0;
    bool multiFlag = true; //멀티어택
    int invokeCnt = 0;
    int multi = 5;
    bool rainFlag = true; //레인드롭
    int DropMax = 3;
    float rainTime = 9.5f;
    bool changeFlag = true; //페이크노
    bool doubleFlag = true; //동시입력노트
    [SerializeField] Transform tfNoteAppear = null; //노트가 생성되는곳
    [SerializeField] GameObject[] goNote = null; //노트 프리펩들
    [SerializeField] GameObject Drop = null; //물방울
    [SerializeField] GameObject[] doubleNote = null; //더블노트 프리펩들
    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager thecomboManager;
    Monster theMonster;
    public static int redTurn = 5;
    public static int blueTurn = 5;


    public NoteManager()
    {
        redTurn = 5;
        blueTurn = 5;
        noteCount = 0;
        noteOn = true;
        currentTime = 0d;
    }


void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        thecomboManager = FindObjectOfType<ComboManager>();
    }

    void Update()
    {
        if(PlayerController.nowMonster != null) theMonster =PlayerController.nowMonster.GetComponent<Monster>();

        if (PlayerController.flag != 3)
        {
            if (noteCount % 10 == 0 && noteCount != 0 && multiFlag && theMonster.multi)
            {
                Multistrike();
            }
            if (noteCount % 10 == 0 && noteCount != 0 && rainFlag && theMonster.drop)
            {
                rainTime = (60f / bpm)*20 + 0.5f;
                Debug.Log("rainTime =" + rainTime);
                RainDrop();
            }
            

            if ((noteCount % (redTurn + blueTurn)) < redTurn) 
            {
                MakeNode(0);
             }
            if ((noteCount % (redTurn + blueTurn)) >= redTurn) 
            {
                MakeNode(1);
             }
            if (PlayerController.nowMonster != null)
            {
                if (noteCount % 20 == 0 && PlayerController.nowMonster.GetComponent<Monster>().Boss)
                {//보스페턴체인지
                    PlayerController.nowMonster.GetComponent<Monster>().BossPatten(PlayerController.nStage);
                }
            }
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
        t_note.transform.SetAsFirstSibling();
        theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가
        invokeCnt++;
    }

    public int MakeNode(int turn)//(int attackTurn , int defenseTurn)
    {
        if (PlayerController.pStage == 4)
        {
            bpm = CenterFlame.instance.boss[PlayerController.nStage].bpm;
        }
        else
        {
            bpm = CenterFlame.instance.bgms[PlayerController.nStage].bpm;
        }

        currentTime += Time.deltaTime;
        if (noteOn == true)
        {
            if (currentTime >= 60d / bpm)  //bpm마다 노트 생성
            {

                if (noteCount % 4 == 0 && noteCount != 0 && doubleFlag && theMonster.doubleNote)
                {
                    DoubleNote();
                }
                else
                {
                    if (turn == 0) arrowDirection = Random.Range(0, 4); // 방어턴
                    else if (turn == 1) arrowDirection = Random.Range(4, 8); //공격턴 

                    GameObject t_note = Instantiate(goNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // 노트를 생성
                    t_note.transform.SetParent(this.transform);
                    t_note.transform.SetAsFirstSibling();
                    
                    theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가

                    if (noteCount % 4 == 0 && noteCount != 0 && changeFlag && theMonster.change)//페이크 노트
                    {
                        float rand = (float)Random.Range(10, 18) / 10;//1,0~1,8초
                        StartCoroutine(CoNoteChange(t_note, rand));
                    }
                }

                currentTime -= 60d / bpm; //-하지않고 0으로설정하면 시차가 생김
                noteCount++; //noteCount증가
                multiFlag = true;
                rainFlag = true;
                doubleFlag = true;
            }
        }

        return noteCount;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Note"))
        {

            if (collision.GetComponent<Note>().GetNoteFlag() && !TimingManager.immortal)//이미지가 있을때만
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

    public void NoteChange(GameObject thisnote)
    {
        int turn = thisnote.GetComponent<Note>().noteType;

        if (turn == 1) arrowDirection = Random.Range(0, 4);
        else if (turn == 0) arrowDirection = Random.Range(4, 8);
        //반대로생성
        GameObject t_note = Instantiate(goNote[arrowDirection], thisnote.transform.position, Quaternion.identity); // 노트를 생성
        t_note.transform.SetParent(this.transform);
        t_note.transform.SetAsLastSibling();
        theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가
        theTimingManager.boxNoteList.Remove(thisnote); // 리스트에 추가
        Destroy(thisnote);
    }

    public void ClearTime()
    {
        currentTime = 0d;
    }

    IEnumerator CoNoteChange(GameObject thisnote, float rand) //코루틴
    {
        yield return new WaitForSeconds(rand);
        NoteChange(thisnote);
    }
    public void Multistrike() //연타
    {
        noteOn = false;
        multiFlag = false;
        arrowDirection = Random.Range(0, 4);
        InvokeRepeating(nameof(SpawnNote), 60f / bpm, 0.1f);
    }
    void NoteOn()
    {
        noteOn= true;
    }

    public void DoubleNote() //이중
    {
        arrowDirection = Random.Range(0, 8);

        GameObject t_note = Instantiate(doubleNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // 노트를 생성
        t_note.transform.SetParent(this.transform);
        t_note.transform.SetAsFirstSibling();
        theTimingManager.boxNoteList.Add(t_note); // 리스트에 추가
    }


    public void RainDrop() //물방울
    {
        rainFlag = false;

        float randomX = Random.Range(0f, 1920f);
        float randomY = Random.Range(0f, 200f);
        GameObject[] p_Drops = new GameObject[DropMax+1];


        randomX = Random.Range(830f, 1020f);
        randomY = Random.Range(50f, 200f);
        p_Drops[0] = Instantiate(Drop, new Vector3(randomX, randomY, 0f), Quaternion.identity);
        p_Drops[0].transform.SetParent(this.transform);
        Destroy(p_Drops[0], rainTime);

        for (int n = 1; n <= DropMax; n++)
        {
            randomX = Random.Range(0f, 1650f);
            randomY = Random.Range(50f, 200f);
            p_Drops[n]=Instantiate(Drop,  new Vector3(randomX, randomY ,0f), Quaternion.identity);
            p_Drops[n].transform.SetParent(this.transform);
            Destroy(p_Drops[n], rainTime);
        }

    }



}
