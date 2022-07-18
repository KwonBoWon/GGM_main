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
        if (Input.GetMouseButtonDown(0))//���콺 ��Ŭ��
        {
            GameObject tempObj = null;//���ӿ�����Ʈ ������
            if (backgroundNum >= 0 && backgroundNum <= 3)
            {
                tempObj = GameObject.Find(backgrounds[backgroundNum]) ;//���ӿ�����Ʈ�� ã��
                if (tempObj != null)
                {
                    Debug.Log("get object" + tempObj.name);
                    tempObj.GetComponent<FadeOut>().StartCoroutine("Fade");
                }
                else
                {
                    Debug.LogError(backgroundNum.ToString() + "error");
                }
            }
            else
            {
                Debug.LogError("Wrong number");
            }


            backgroundNum++;
        }
    }
}
