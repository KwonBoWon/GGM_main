using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Tab : MonoBehaviour
{
    public Image image;
    Image tmpimage;
    public Image[] cover;
    public bool[] collect = new bool[6];
    public static int tabck = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.image = GetComponent<Image>();
        bool[] collect = Enumerable.Repeat(false, 6).ToArray(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { //탭키 누르는 순간 보이게
            this.image.enabled = true;
            Time.timeScale = 0.0F;
            tabck = 0;
            CenterFlame.instance.bgms[PlayerController.nStage].source.Pause();
        }
        if (Input.GetKeyUp(KeyCode.Tab)) { //탭키 떼는 순간 안 보이게
            this.image.enabled = false;
            tabck = 1;
            if (ESC.ESCck != 0) {
                Time.timeScale = 1.0F;
                CenterFlame.instance.bgms[PlayerController.nStage].source.Play();
            }
        }
        
    }
    public void change(int n) {
        Destroy(cover[n]);
    }
}
