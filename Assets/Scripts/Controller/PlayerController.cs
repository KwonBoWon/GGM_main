﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MonsterList
{
    public GameObject[] Monsters;
}


public class PlayerController : MonoBehaviour
{

    GameObject nowPlayer;
    public Sprite[] Death;
    public Animator Fadeout;
    public GameObject FadePannel;
    bool AniCk = true;
    bool Bad = true;
    public MonsterList[] goMonster;
    [SerializeField] Animator[] player;

    TimingManager theTimingManager;
    private Tree T = new Tree();

    public static GameObject nowMonster;
    public static GameObject nowBackGround;
    GameObject[] del;

    public static int flag = 0; //배경 맵 플레그
    public static int nStage = 1; //스테이지 1423
    public static int pStage = 0; //몇번째미로인지(적 강해지는) 1234

    GameObject backGround;
    SpriteRenderer backRenderer;
    Animator BackGround1 = null;
    Animator BackGround2 = null;
    Animator BackGround3 = null;
    [SerializeField] Transform tfMonsterAppear = null;
    [SerializeField] GameObject Tree;
    [SerializeField] GameObject[] weapon = null;

    int[] weaponDamage = new int[3];
    [SerializeField] Transform LWeapon = null;
    [SerializeField] Transform RWeapon = null;
    [SerializeField] Transform Weapons = null;


    //public static GameObject nowWeapon = null;
    public static int nowWeapon = 0;

    GameObject LeftW;
    GameObject RightW;

    BackGroundManager thebackGroundManager;
    MonsterManager theMonsterManager;
    public Slider MonsterHP;
    public Slider TimeHP;
    public static int cnt = 0;

    public float maxHP = 100; //최대 체력
    public float curHP = 100; //현재 체력

    int Lrand = 0, Rrand = 1;

    public static float maxTime = 100; //최대 시간
    public static float curTime = 100; //현재시간
    private float addTime = 40; //시간추가
    public static float playerDamage = 10;
    private bool monsterLife = true;
    bool makeWeapon = true;
    bool crossroad = true;

    Tab theTab;
    public GameObject wall;

    public PlayerController()
    {
        flag = 0; //배경 맵 플레그
        nStage = 1; //스테이지 1423
        pStage = 0; //몇번째미로인지(적 강해지는) 1234
        nowWeapon = 0;
        cnt = 0;
        maxTime = 100; //최대 시간
        curTime = 100; //현재시간
        playerDamage = 10;
    }


    void Start()
    {
        theTab = FindObjectOfType<Tab>();
        nowMonster = Instantiate(goMonster[nStage].Monsters[cnt], tfMonsterAppear.position, Quaternion.identity);
        nowMonster.transform.SetParent(this.transform);

        MonsterHP = GameObject.Find("MonsterHP").GetComponent<Slider>();
        MonsterHP.value = (float)curHP / (float)maxHP;
        TimeHP = GameObject.Find("Time").GetComponent<Slider>();
        TimeHP.value = (float)curTime / (float)maxTime; //초기시간

        theTimingManager = FindObjectOfType<TimingManager>();
        thebackGroundManager = FindObjectOfType<BackGroundManager>();

        weaponDamage[0] = 10;
        weaponDamage[1] = 10;
        weaponDamage[2] = 10;




    }
    void Update()
    {
        //화살표 입력 0123 : DULR
        if (flag == 3 && pStage != 3 && crossroad)
        {//갈림길일때
            CrossRoad();
        }
        else//갈림길이 아닐때
        {
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
            if (Input.GetKeyDown(KeyCode.C)) //C누르면 노트클리어
            {
                CenterFlame.instance.NoteClear();
            }





            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow))//상좌
            {
                theTimingManager.CheckTiming(10);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                theTimingManager.CheckTiming(10);
            }

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.UpArrow))//상우
            {
                theTimingManager.CheckTiming(11);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                theTimingManager.CheckTiming(11);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.DownArrow))//하좌
            {
                theTimingManager.CheckTiming(12);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                theTimingManager.CheckTiming(12);
            }
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow))//하우
            {
                theTimingManager.CheckTiming(13);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                theTimingManager.CheckTiming(13);
            }


        }
        HandleHP();
        HandleTime();
    }
    public void monsterDie() //적즉사
    {
        curHP = 0;
    }
    public void HandleHP()
    {
        MonsterHP.value = (float)curHP / (float)maxHP;

        if (MonsterHP.value == 0 && monsterLife == true)
        { // 적 죽으면
            Invoke(nameof(DeathSound), 0.1f);
            MonsterHP.gameObject.SetActive(false);
            monsterLife = false;
            NoteManager.noteOn = false;
            CenterFlame.instance.NoteClear();
            if (pStage == 4)
            { //최종 보스 죽으면
                Animator MonAni = nowMonster.GetComponent<Animator>();
                MonAni.SetTrigger("Die");  //보스 사망 모션
                Invoke(nameof(ToEnding), 0.5f);
                Invoke(nameof(ChangeScene), 1.1f);
            }
            else
            {
                del = GameObject.FindGameObjectsWithTag("Monster");
                foreach (GameObject note in del)
                {
                    Destroy(note, 0.6f);
                }
                cnt++;
                if (cnt == 3) cnt = 0;
                Invoke(nameof(BackGroundChange), 0.7f);
            }
        }
    }
    public void DeathSound()
    {
        nowMonster.GetComponent<Monster>().Sounds[2].source.Play();
    }

    public void HandleTime()
    {
        TimeHP.value = (float)curTime / (float)maxTime * (1 + ((float)0.5 * pStage));//3
        if (TimeHP.value > 0.0f)
        {
            if (flag != 3) //갈림길일때
                curTime -= Time.deltaTime; //시간 줄어듦
        }
        else //시간없을때(죽었을때)
        {
            SoundEffectManager.instance.Sounds[2].source.Play();
            //스테이지 노래도 느려지게 하면 좋을 듯
            Time.timeScale = 0.5F;
            if (AniCk)
            {
                GameObject.Find("PlayerParent").transform.Find("Player" + nowWeapon).gameObject.SetActive(false);
                GameObject.Find("PlayerParent").transform.Find("Player3").gameObject.SetActive(true);
                FadePannel.transform.SetAsLastSibling();
                Fadeout.SetTrigger("hit");
                AniCk = false;
            }
            Invoke(nameof(ChangeScene), 1.1f);
        }
    }

    public void MakeMonster()
    {

        NoteManager.noteCount = 0;
        NoteManager.currentTime = 0d;
        monsterLife = true;
        nowMonster = Instantiate(goMonster[nStage].Monsters[cnt], tfMonsterAppear.position, Quaternion.identity);
        nowMonster.transform.SetParent(this.transform);
        maxHP = curHP = nowMonster.GetComponent<Monster>().monsterHP * (1 + ((float)0.25 * pStage));  //몬스터 스크립트 + 스테이지체력보정
        Debug.Log("maxHP" + maxHP);
        MonsterHP.gameObject.SetActive(true);

        curTime += addTime; //시간추가
        if (curTime > maxTime) curTime = maxTime;
        NoteManager.noteOn = true;

        NoteManager.redTurn = nowMonster.GetComponent<Monster>().redTurn;
        NoteManager.blueTurn = nowMonster.GetComponent<Monster>().blueTurn;
    }

    public void CrossRoad()
    {

        if (pStage == 2 && nStage == 4 && theTab.collectionData.SheetMusic < 3)
        {
            wall.SetActive(true);
        }
        if (pStage != 4)
            MakeWeapon(); //무기생성 
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //오른쪽 애니메이션 나오게
            nStage = T.ChooseR();//오른쪽노드
            backGround = GameObject.Find(nStage + "-1");
            BackGround1 = backGround.GetComponent<Animator>();
            GameObject.Find("Player" + nowWeapon).gameObject.SetActive(false);
            nowWeapon = Rrand;
            GameObject.Find("PlayerParent").transform.Find("Player" + nowWeapon).gameObject.SetActive(true);
            player[nowWeapon].SetTrigger("CR"); //오른쪽으로 움직이는 애니메이션
            Invoke(nameof(ChangeStage), 1.1f);
            Invoke(nameof(Walloff), 1.1f);
            crossroad = false;

        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !(pStage == 2 && nStage == 4 &&theTab.collectionData.SheetMusic <3))
        {
            //왼쪽 애니메이션 나오게
            nStage = T.ChooseL();//왼쪽노드
            backGround = GameObject.Find(nStage + "-1");
            BackGround1 = backGround.GetComponent<Animator>();
            GameObject.Find("Player" + nowWeapon).gameObject.SetActive(false);
            nowWeapon = Lrand;
            GameObject.Find("PlayerParent").transform.Find("Player" + nowWeapon).gameObject.SetActive(true);
            player[nowWeapon].SetTrigger("CL"); //왼쪽으로 움직이는 애니메이션
            Invoke(nameof(ChangeStage), 1.1f);
            Invoke(nameof(Walloff), 1.1f);
            crossroad = false;
        }

    }
    public void BackGroundChange()
    {
        if (pStage == 3 && flag == 2)
        { //보스 생성
            CenterFlame.instance.StopMusic();
            //flag++;
            pStage++;

            BackGround3.SetTrigger("hit3");
            nStage = T.ChooseL();
            backGround = GameObject.Find(nStage + "-3");
            BackGround3 = backGround.GetComponent<Animator>();
            BackGround3.SetTrigger("boss");
            cnt = 3;
            Invoke(nameof(MakeMonster), 0.8f);
        }
        else if (flag == 0)
        {
            crossroad = true;
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
            MakeMonster(); //두번째몬스터
            return;
        }
        else if (flag == 1)
        {
            BackGround2.SetTrigger("hit2");
            BackGround3.SetTrigger("hit2");
            flag++;
            MakeMonster(); //중간보스
            return;
        }
        else if (flag == 2)
        {
            //시간증가

            curTime = curTime * (1 + ((float)0.5 * pStage));
            maxTime = maxTime * (1 + ((float)0.5 * pStage));
            addTime = addTime * (1 + ((float)0.5 * pStage));

            Debug.Log("curTime =" + curTime);
            Debug.Log("maxTime =" + maxTime);
            Debug.Log("addTime =" + addTime);



            backGround = GameObject.Find(nStage + "-4");
            BackGround2 = backGround.GetComponent<Animator>();
            BackGround2.SetTrigger("hit1");
            BackGround3.SetTrigger("hit3");

            flag++;
            return;
        }

    }
    public void MakeWeapon()
    {
        if (makeWeapon)
        {
            makeWeapon = false;
            Lrand = Random.Range(0, 3);
            do
            {
                Rrand = Random.Range(0, 3);
            } while (Lrand == Rrand);
            if (!(pStage == 2 && nStage == 4 && theTab.collectionData.SheetMusic < 3))
            {
                LeftW = Instantiate(weapon[Lrand], LWeapon.position, Quaternion.identity);
                LeftW.transform.SetParent(Weapons);
            }
            RightW = Instantiate(weapon[Rrand], RWeapon.position, Quaternion.identity);

            RightW.transform.SetParent(Weapons);
        }
    }
    public void ChangeStage()
    {
        GameObject.Find("Backgrounds").transform.Find(nStage - 1 + "-color").gameObject.SetActive(false);
        BackGround2.SetTrigger("hit2"); //갈림길 사라지게
        BackGround1.SetTrigger("hit1"); //새로운 스테이지 처음 맵 보이게
        GameObject.Find("Backgrounds").transform.Find(nStage + "-2").gameObject.SetActive(true);
        GameObject.Find("Backgrounds").transform.Find(nStage + "-3").gameObject.SetActive(true);
        GameObject.Find("Backgrounds").transform.Find(nStage + "-color").gameObject.SetActive(true);
        MakeMonster(); //첫번째몬스터
        flag = 0;
        pStage++;
        makeWeapon = true;

        playerDamage = weaponDamage[Lrand];// + pStage * 5; //공격력보정 일단보류


        Destroy(LeftW);
        Destroy(RightW);
    }
    public void ChangeScene()
    {
        if (Bad)
            SceneManager.LoadScene("GameOver");
        else if (nStage == 4)
            SceneManager.LoadScene("JinEnding");
        else
            SceneManager.LoadScene("NormalEnding");
    }

    public void ChangeNoteSpeed()
    {

    }

    void ToEnding()
    {
        Time.timeScale = 0.5F;
        Bad = false;
        if (AniCk)
        {
            
            FadePannel.transform.SetAsLastSibling();
            Fadeout.SetTrigger("hit");
            AniCk = false;
        }
        Tab.nstage = nStage;
    }

    void Walloff()
    {
        wall.SetActive(false);
    }
}