using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ESC : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image; 
    public static int ESCck = 1;
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ESCck == 1) { //esc키 누르는 순간 보이게
            this.image.enabled = true;
            Time.timeScale = 0.0F;
            ESCck = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ESCck == 0) { //esc키 떼는 순간 안 보이게
            this.image.enabled = false;
            Time.timeScale = 1.0F;
            ESCck = 1;
        }
    }
}
