using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    public GameObject monster;
    public Slider MonsterHP;
    public float maxHP = 100; //최대 체력
    public float curHP = 100; //현재 체력
    void Start()
    {
        MonsterHP = GameObject.Find("MonsterHP").GetComponent<Slider>();
        MonsterHP.value = (float) curHP / (float) maxHP;
        theTimingManager = FindObjectOfType<TimingManager>();//������Ʈ�� ã��(TimingManager)
    }
    void Update()
    {
        //화살표 입
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            theTimingManager.CheckTiming(0);
            //Debug.Log("down");

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            theTimingManager.CheckTiming(1);
            //Debug.Log("up");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            theTimingManager.CheckTiming(2);
            //Debug.Log("left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            theTimingManager.CheckTiming(3);
            //Debug.Log("right");
        }
        HandleHP();

    }
    public void HandleHP() {
        MonsterHP.value = (float) curHP / (float) maxHP;
        if (MonsterHP.value == 0) { // 적 죽으면

            monster.SetActive(false);
            MonsterHP.gameObject.SetActive(false);
        }
    }


}
