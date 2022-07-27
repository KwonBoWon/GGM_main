using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    [SerializeField] Transform tfMonsterAppear = null;
    [SerializeField] GameObject[] goMonster = null;
    int cnt = 0;
    BackGroundManager thebackGroundManager;
    public Slider MonsterHP;
    public Slider TimeHP;

    public float maxHP = 100; //최대 체력
    public float curHP = 100; //현재 체력

    public float maxTime = 50; //최대 시간
    public float curTime = 50; //현재시간
    public float addTime = 15; //시간추가
    private bool monsterLife = true;

    void Start()
    {
        Instantiate(goMonster[cnt], tfMonsterAppear.position, Quaternion.identity);
        MonsterHP = GameObject.Find("MonsterHP").GetComponent<Slider>();
        MonsterHP.value = (float) curHP / (float) maxHP;
        TimeHP = GameObject.Find("Time").GetComponent<Slider>();
        TimeHP.value =(float)curTime / (float)maxTime; //초기시간


        theTimingManager = FindObjectOfType<TimingManager>();
        thebackGroundManager = FindObjectOfType<BackGroundManager>();
    }
    void Update()
    {
        //화살표 입력 0123 : DULR
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            theTimingManager.CheckTiming(0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            theTimingManager.CheckTiming(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            theTimingManager.CheckTiming(2);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            theTimingManager.CheckTiming(3);
        }
        HandleHP();
        HandleTime();
    }
    public void HandleHP() {
        MonsterHP.value = (float) curHP / (float) maxHP;
           
        if (MonsterHP.value == 0 && monsterLife == true) { // 적 죽으면
         	Destroy(GameObject.Find("monster" + cnt + "(Clone)"));
            cnt++;
           // Debug.Log(cnt);
            MonsterHP.gameObject.SetActive(false);

            NoteManager.noteOn = false;
            CenterFlame.instance.StopMusic();
            theTimingManager.boxNoteList.Clear();
            thebackGroundManager.ChangeBackground();
            NoteManager.noteOn = true;

            monsterLife = false;
            MakeMonster();
        }
    }
    public void HandleTime()
    {
        TimeHP.value = (float) curTime / (float) maxTime;
        if (TimeHP.value > 0.0f)
        {
            curTime -=Time.deltaTime; //시간 줄어듦
        }
        else //시간없을때(죽었을때)
        {
            
        }
    }

    public void MakeMonster() {
        curHP = 100;
        monsterLife = true;
        Instantiate(goMonster[cnt], tfMonsterAppear.position, Quaternion.identity);
        MonsterHP.gameObject.SetActive(true);
        //monsterhp=Instantiate(MonsterHP, tfMonsterAppear.position , Quaternion.identity);


       
        curTime += addTime; //시간추가
        if(curTime>maxTime) curTime = maxTime;
    }

}
