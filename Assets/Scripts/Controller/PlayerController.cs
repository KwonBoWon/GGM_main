using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    public static int flag = 0;
    int nStage = 1;
    GameObject backGround;
    Animator BackGround1 = null;
    Animator BackGround2 = null;
    Animator BackGround3 = null;
    [SerializeField] Transform tfMonsterAppear = null;
    [SerializeField] GameObject[] goMonster = null;
    public static int cnt = 0; //몬스터 나오는 횟수 3의 배수일때 갈림길
    BackGroundManager thebackGroundManager;
    MonsterManager theMonsterManager;
    public Slider MonsterHP;
    public Slider TimeHP;

    GameObject t_monstet;

    public float maxHP = 100; //최대 체력
    public float curHP = 100; //현재 체력

    public float maxTime = 50; //최대 시간
    public float curTime = 50; //현재시간
    public float addTime = 30; //시간추가
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
        if (Input.GetKeyDown(KeyCode.K)) //K누르면 즉사
        {
            monsterDie();
        }
        if (Input.GetKeyDown(KeyCode.C)) //K누르면 즉사
        {
            CenterFlame.instance.NoteClear();
        }
        HandleHP();
        HandleTime();
        if (flag == 3)
            CrossRoad();
    }
    public void monsterDie() //적즉사
    {
        curHP = 0;
    }
    public void HandleHP() {
        MonsterHP.value = (float) curHP / (float) maxHP;
           
        if (MonsterHP.value == 0 && monsterLife == true) { // 적 죽으면
         	Destroy(GameObject.Find("monster" + cnt + "(Clone)"));
            cnt++;
           // Debug.Log(cnt);
            MonsterHP.gameObject.SetActive(false);

            NoteManager.noteOn = false;
            //CenterFlame.instance.StopMusic();
            CenterFlame.instance.NoteClear();
            theTimingManager.boxNoteList.Clear();
           //thebackGroundManager.ChangeBackground();
           BackGroundChange();
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
        NoteManager.noteOn = true;
    }

    public void MonsterLifeTrue() {
        monsterLife = true;
    }
    public void CrossRoad() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) { 
            //오른쪽 애니메이션 나오게
            nStage++;
            backGround = GameObject.Find(nStage + "-1");
            BackGround1 = backGround.GetComponent<Animator>();
            BackGround2.SetTrigger("hit2"); //갈림길 사라지게
            BackGround1.SetTrigger("hit1"); //새로운 스테이지 처음 맵 보이게
            MakeMonster();
            flag = 0;
            Debug.Log(nStage);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            //왼쪽 애니메이션 나오게
            nStage++;
            backGround = GameObject.Find(nStage + "-1");
            BackGround1 = backGround.GetComponent<Animator>();
            BackGround2.SetTrigger("hit2"); //갈림길 사라지게
            BackGround1.SetTrigger("hit1"); //새로운 스테이지 처음 맵 보이게
            MakeMonster();
            flag = 0;
            Debug.Log(nStage);
        }
    }
    public void BackGroundChange() {
        if (flag == 0) {
            backGround = GameObject.Find(nStage + "-1");
            BackGround1 = backGround.GetComponent<Animator>();
            backGround = GameObject.Find(nStage + "-2");
            BackGround2 = backGround.GetComponent<Animator>();                
            backGround = GameObject.Find(nStage + "-3");
            BackGround3 = backGround.GetComponent<Animator>();
            BackGround1.SetTrigger("hit");
            BackGround2.SetTrigger("hit1");
            BackGround3.SetTrigger("hit1");
            flag++;
            MakeMonster();
            return;
        }
        else if (flag == 1) {
            BackGround2.SetTrigger("hit2");
            BackGround3.SetTrigger("hit2");
            flag++;
            MakeMonster();
            return;
        }
        else if (flag == 2) {
            backGround = GameObject.Find(nStage + "-4"); 
            BackGround2 = backGround.GetComponent<Animator>(); //갈림길 애니메이션
            BackGround2.SetTrigger("hit1");
            BackGround3.SetTrigger("hit3");
            flag++;
            return;
        }
        
    }
    
}