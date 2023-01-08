using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    S_TimingManager S_theTimingManager;
    
    void Start()
    {
        S_theTimingManager = FindObjectOfType<S_TimingManager>();//������Ʈ�� ã��(TimingManager)
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            S_theTimingManager.CheckTiming();
        }
    }
}
