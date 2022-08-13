using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTime : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtClearTime = null; //ÄŞº¸ ÅØ½ºÆ®
    float sec = 0;
    int min;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        txtClearTime.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

        if ((int)sec > 59)
        {
            sec = 0;
            min++;

        }
    }
}
