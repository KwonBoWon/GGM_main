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
        if (Input.GetKeyDown(KeyCode.Space))
            //�����̽��� �Է��Ҷ����� CheckTimingȣ��
        {
            theTimingManager.CheckTiming();
            
        }
    }
}
