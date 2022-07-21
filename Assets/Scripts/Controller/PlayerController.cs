using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    public Slider MonsterHP;
    private float maxHP = 100; //최대 체력
    private float curHP = 100; //현재 체력
    void Start()
    {
        MonsterHP.value = (float) curHP / (float) maxHP;
        theTimingManager = FindObjectOfType<TimingManager>();//������Ʈ�� ã��(TimingManager)
    }
    void Update()
    {
        //ȭ��ǥ�Է�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            theTimingManager.CheckTiming(0); 
            curHP -= 10; //현재 체력 깎는 걸 판정 코드에 넣는 게 더 좋을 듯
            Debug.Log("down");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            theTimingManager.CheckTiming(1);
            curHP -= 10;
            Debug.Log("up");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            theTimingManager.CheckTiming(2);
            curHP -= 10;
            Debug.Log("left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            theTimingManager.CheckTiming(3);
            curHP -= 10;
            Debug.Log("right");
        }

        HandleHP();
    }
    private void HandleHP() {
        MonsterHP.value = (float) curHP / (float) maxHP;
    }
}
