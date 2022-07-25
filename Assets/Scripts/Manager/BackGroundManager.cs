using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public string[] backgrounds;
    public int backgroundNum = 0;
    public static int stage = 0;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))//마우스 좌클릭
        {

        }
    }


    public void ChangeBackground()
    {
        GameObject tempObj = null;//게임오브젝트 담을곳
        if (backgroundNum >= 0 && backgroundNum <= 3)
        {
            tempObj = GameObject.Find(backgrounds[stage]);//게임오브젝트를 찾음
            if (tempObj != null)
            {
                tempObj.GetComponent<FadeOut>().StartCoroutine("Fade");
            }
        }
        stage++;
    }
}