using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();//������Ʈ�� ã��(TimingManager)
    }
    void Update()
    {
        //ȭ��ǥ�Է�
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


    }
}
