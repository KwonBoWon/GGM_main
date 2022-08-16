using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MonsterList
{
    public GameObject[] Monsters;
}


public class PlayerController : MonoBehaviour
{
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
    public float addTime = 40; //시간추가
    public static float playerDamage = 10;
    private bool monsterLife = true;
    bool makeWeapon = true;

    void Start()
    {
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
        if (flag == 3 && pStage != 3) //갈림길일때
            CrossRoad();
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
            nowMonster.GetComponent<Monster>().Sounds[2].source.Play();

            del = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject note in del)
            {
                Destroy(note);
            }
            cnt++;
            if (cnt == 3) cnt = 0;
            MonsterHP.gameObject.SetActive(false);
            monsterLife = false;
            NoteManager.noteOn = false;
            //CenterFlame.instance.StopMusic();
            CenterFlame.instance.NoteClear();
            //thebackGroundManager.ChangeBackground();
            BackGroundChange();
        }
    }
    public void HandleTime()
    {
        TimeHP.value = (float)curTime / (float)maxTime;
        if (TimeHP.value > 0.0f)
        {
            if (flag != 3) //갈림길일때
                curTime -= Time.deltaTime; //시간 줄어듦
        }
        else //시간없을때(죽었을때)
        {
            SoundEffectManager.instance.Sounds[2].source.Play();
        }
    }

    public void MakeMonster()
    {
        NoteManager.noteCount = 0;
        monsterLife = true;
        nowMonster = Instantiate(goMonster[nStage].Monsters[cnt], tfMonsterAppear.position, Quaternion.identity);
        nowMonster.transform.SetParent(this.transform);
        maxHP = curHP = nowMonster.GetComponent<Monster>().monsterHP + pStage * 20;
        //몬스터 스크립트 + 스테이지체력보정
        MonsterHP.gameObject.SetActive(true);

        curTime += addTime; //시간추가
        if (curTime > maxTime) curTime = maxTime;
        NoteManager.noteOn = true;

        NoteManager.redTurn = nowMonster.GetComponent<Monster>().redTurn;
        NoteManager.blueTurn = nowMonster.GetComponent<Monster>().blueTurn;
    }

    public void CrossRoad()
    {   
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
            Invoke("ChangeStage", 1.1f);


        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //왼쪽 애니메이션 나오게
            nStage = T.ChooseL();//왼쪽노드
            backGround = GameObject.Find(nStage + "-1");
            BackGround1 = backGround.GetComponent<Animator>();
            GameObject.Find("Player" + nowWeapon).gameObject.SetActive(false);
            nowWeapon = Lrand;
            GameObject.Find("PlayerParent").transform.Find("Player" + nowWeapon).gameObject.SetActive(true);
            player[nowWeapon].SetTrigger("CL"); //왼쪽으로 움직이는 애니메이션
            Invoke("ChangeStage", 1.1f);

        }
    }
    public void BackGroundChange() {
        if (pStage == 3 && flag == 2) { //보스 생성
            //flag++;
            pStage++;
            Debug.Log(pStage);
            BackGround3.SetTrigger("hit3");
            nStage = T.ChooseL();
            backGround = GameObject.Find(nStage + "-3");
            BackGround3 = backGround.GetComponent<Animator>();
            BackGround3.SetTrigger("boss");
            cnt = 3;
            Invoke("MakeMonster", 0.8f);
        }
        else if (flag == 0)
        {
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
        else if (flag == 1)
        {
            BackGround2.SetTrigger("hit2");
            BackGround3.SetTrigger("hit2");
            flag++;
            MakeMonster();
            return;
        }
        else if (flag == 2)
        {
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

            LeftW = Instantiate(weapon[Lrand], LWeapon.position, Quaternion.identity);
            RightW = Instantiate(weapon[Rrand], RWeapon.position, Quaternion.identity);
            LeftW.transform.SetParent(Weapons);
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
        MakeMonster();
        flag = 0;
        pStage++;
        makeWeapon = true;

        playerDamage = weaponDamage[Lrand];// + pStage * 5; //공격력보정 일단보류


        Destroy(LeftW);
        Destroy(RightW);
    }

}
