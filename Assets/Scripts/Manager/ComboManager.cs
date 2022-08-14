using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] heart; //하트 프리펩
    [SerializeField] UnityEngine.UI.Text txtCombo = null; //콤보 텍스트
    GameObject Obj;
    public int curCombo = 0; //콤보스택
    int heartCnt=0; //하트 개수(최대3)
    int comboStack =10; //comboStack개마다 콤보 생성
    int comboDamage = 15;


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
        heart[0].SetActive(false);
        heart[1].SetActive(false);
        heart[2].SetActive(false);
    }

    
    public void IncreaseCombo(int p_num = 1)
    {
        curCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", curCombo);//000,000형식

        if (curCombo >= comboStack && curCombo % comboStack == 0)//10개마다 콤보생성
        {
            if (heartCnt < 3){
                heart[heartCnt].SetActive(true);
                heartCnt++;
            }
        }
        if (curCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
        }
    }

    public void ResetCombo()
    {
        curCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
    }

    public void UseCombo()
    {
        if (heartCnt != 0)
        {
            heart[heartCnt-1].SetActive(false);
            heartCnt--;
            //여기에 콤보 사용
            if (PlayerController.nowWeapon == 2) WandCombo();
            else if (PlayerController.nowWeapon == 0) SwordCombo();
            else if (PlayerController.nowWeapon == 1) BatCombo();

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
        Obj.GetComponent<PlayerController>().curHP -= curCombo + 10;
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
    }
    public void Immortal() {
        TimingManager.immortal=false;
    }




}
