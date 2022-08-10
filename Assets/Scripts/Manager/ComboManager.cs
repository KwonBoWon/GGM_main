using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] heart;

    [SerializeField] UnityEngine.UI.Text txtCombo = null;

    public int curCombo = 0;
    int heartCunt=0;

    private void Start()
    {
        txtCombo.gameObject.SetActive(false);
        heart[0].SetActive(false);
        heart[1].SetActive(false);
        heart[2].SetActive(false);

        //goComboImage.SetActive(false);
    }

    
    public void IncreaseCombo(int p_num = 1)
    {
        curCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", curCombo);

        if (curCombo >= 10 && curCombo % 10 == 0)
        {
            if (heartCunt < 3){
                heart[heartCunt].SetActive(true);
                heartCunt++;
            }
        }

        if (curCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
           //goComboImage.SetActive(true);
        }
    }

    public void ResetCombo()
    {
        curCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        //goComboImage.SetActive(false);
    }

}
