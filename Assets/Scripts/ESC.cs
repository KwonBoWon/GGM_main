using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ESC : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image; 
    public Slider Volume;
    public static int ESCck = 1;
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ESCck == 1) { //esc키 누르면 보이게
            this.image.enabled = true;
            Time.timeScale = 0.0F;
            ESCck = 0;
            Volume.gameObject.SetActive(true);
            CenterFlame.instance.bgms[PlayerController.nStage].source.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ESCck == 0) { //esc키 또 누르면 없어지게
            this.image.enabled = false;
            ESCck = 1;
            if (Tab.tabck != 0) {
                Time.timeScale = 1.0F;
                CenterFlame.instance.bgms[PlayerController.nStage].source.Play();
            }
            Volume.gameObject.SetActive(false);
        }
    }
}
