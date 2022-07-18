using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();//오브젝트를 찾음(TimingManager)
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            //스페이스바 입력할때마다 CheckTiming호출
        {
            theTimingManager.CheckTiming();
            
        }
    }
}
