using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] heart; //하트 프리펩
    [SerializeField] UnityEngine.UI.Text txtCombo = null; //콤보 텍스트
    [SerializeField] UnityEngine.UI.Text txt_combo= null; //콤보
    public Animator[] AttackEffect;
    GameObject Obj;
    public int curCombo = 0; //콤보스택
    int heartCnt=0; //하트 개수(최대3)
    int comboStack =10; //comboStack개마다 콤보 생성
    int comboDamage = 15;
    [Header("효과음 combo sword bat wand")]
    [SerializeField]
    public Sound[] Sounds;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseCombo();
        }
    }
    private void Start()
    {
        Obj = GameObject.Find("PlayerControll");
        txtCombo.gameObject.SetActive(false);
        txt_combo.gameObject.SetActive(false);
        heart[0].SetActive(false);
        heart[1].SetActive(false);
        heart[2].SetActive(false);
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source = gameObject.AddComponent<AudioSource>();
            Sounds[i].source.volume = Sounds[i].volume;
            Sounds[i].source.clip = Sounds[i].clip;
            Sounds[i].source.loop = false;
        }
    }


    
    public void IncreaseCombo(int p_num = 1)
    {
        curCombo += p_num;

        txtCombo.text = string.Format("{0:#,##0}", curCombo);//000,000형식

        if (curCombo >= comboStack && curCombo % comboStack == 0)//10개마다 콤보생성
        {
            txtCombo.color = Color.red;
            if (heartCnt < 3)
            {
                heart[heartCnt].SetActive(true);
                heartCnt++;
            }
        }
        else
        {
            txtCombo.color = Color.white;
        }
        if (curCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
            txt_combo.gameObject.SetActive(true);
        }
    }

    public void ResetCombo()
    {
        curCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        txt_combo.gameObject.SetActive(false);
    }

    public void UseCombo()
    {
        if (heartCnt != 0)
        {
            heart[heartCnt-1].SetActive(false);
            heartCnt--;
            //여기에 콤보 사용
            if (PlayerController.nowWeapon == 2)
            {
                WandCombo();
                AttackEffect[2].SetTrigger("hit");
                if (ESC.gameSound) Sounds[2].source.Play();
            }
            else if (PlayerController.nowWeapon == 0) {
                SwordCombo();
                AttackEffect[0].SetTrigger("hit");
                if (ESC.gameSound) Sounds[0].source.Play();
            }
            else if (PlayerController.nowWeapon == 1) {
                BatCombo();
                AttackEffect[1].SetTrigger("hit");
                if (ESC.gameSound) Sounds[1].source.Play();
            }
            Obj.GetComponent<PlayerController>().curHP -= comboDamage;
        }

    }

    public void WandCombo()// 힐, 잠시무적
    {
        PlayerController.curTime += 30; //시간추가
        if (PlayerController.curTime > PlayerController.maxTime) PlayerController.curTime = PlayerController.maxTime;
        TimingManager.immortal = true;
        Invoke(nameof(Immortal), 2f); //2초간 무적
    }
    public void SwordCombo()//강한공격(현재 콤보 스택만큼 추가로)
    {
        int swordDamage = curCombo+20;
        if(swordDamage>100) swordDamage = 100;//최대 100데미지
        Obj.GetComponent<PlayerController>().curHP -= swordDamage;
    }

    public void BatCombo()//노트클리어(물방울도 지워버림)
    {
        CenterFlame.instance.NoteClear(); //노트클리어

        GameObject[] destroyDrops = null; //물방울 클리어
        if (destroyDrops == null)
        {
            destroyDrops = GameObject.FindGameObjectsWithTag("Drop");
        }
        foreach (GameObject note in destroyDrops)
        {
            Destroy(note);
        }
        Obj.GetComponent<PlayerController>().curHP -= 30;
    }
    public void Immortal() {
        TimingManager.immortal=false;
    }




}
