using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public string[] backgrounds;
    public int backgroundNum = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))//마우스 좌클릭
        {
            GameObject tempObj = null;//게임오브젝트 담을곳
            if (backgroundNum >= 0 && backgroundNum <= 3)
            {
                tempObj = GameObject.Find(backgrounds[backgroundNum]);//게임오브젝트를 찾음
                if (tempObj != null)
                {
                    //Debug.Log("get object " + tempObj.name);
                    tempObj.GetComponent<FadeOut>().StartCoroutine("Fade");
                }
                else
                {
                    //Debug.LogError(backgroundNum.ToString() + "error");
                }
            }
            else
            {
                // Debug.LogError("Wrong number");
            }
            backgroundNum++;
        }
    }
}
