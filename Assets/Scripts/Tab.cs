using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tab : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { //탭키 누르는 순간 보이게
            this.image.enabled = true;
            Time.timeScale = 0.0F;
            CenterFlame.instance.bgms[BackGroundManager.stage].source.Pause();
        }
        if (Input.GetKeyUp(KeyCode.Tab)) { //탭키 떼는 순간 안 보이게
            this.image.enabled = false;
            if (ESC.ESCck != 0)
                Time.timeScale = 1.0F;
            CenterFlame.instance.bgms[BackGroundManager.stage].source.Play();
        }
    }
}
