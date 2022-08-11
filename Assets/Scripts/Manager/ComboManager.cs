using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] heart; //��Ʈ ������
    [SerializeField] UnityEngine.UI.Text txtCombo = null; //�޺� �ؽ�Ʈ

    public int curCombo = 0; //�޺�����
    int heartCnt=0; //��Ʈ ����(�ִ�3)
    int comboStack = 10; //comboStack������ �޺� ����



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseCombo();
        }
    }
    private void Start()
    {
        txtCombo.gameObject.SetActive(false);
        heart[0].SetActive(false);
        heart[1].SetActive(false);
        heart[2].SetActive(false);
    }

    
    public void IncreaseCombo(int p_num = 1)
    {
        curCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", curCombo);//000,000����

        if (curCombo >= comboStack && curCombo % comboStack == 0)//10������ �޺�����
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
            //���⿡ �޺� ���
        }

    }


}
