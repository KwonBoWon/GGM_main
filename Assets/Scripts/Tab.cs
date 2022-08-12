using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Tab : MonoBehaviour
{
    public SpriteRenderer image; //도감창 렌더러를 받음
    public Sprite[] change_img; //변경할 이미지
    public Image[] cover; //수집 안 된 애들 비활성화시키는 이미지
    public bool[] collect = new bool[6];
    public static int tabck = 1;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        bool[] collect = Enumerable.Repeat(false, 6).ToArray(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { //탭키 누르는 순간 보이게
            image.sprite = change_img[6];
            image.enabled = true;
            foreach (Image black in cover) {
                black.enabled = true; //비활성화 시키는 애들
            }
            Time.timeScale = 0.0F;
            tabck = 0;
            CenterFlame.instance.bgms[PlayerController.nStage].source.Pause();
        }
        if (Input.GetKeyUp(KeyCode.Tab)) { //탭키 떼는 순간 안 보이게
            image.enabled = false;
            tabck = 1;
            if (ESC.ESCck != 0) {
                foreach (Image black in cover) {
                    black.enabled = false;
                }
                Time.timeScale = 1.0F;
                image.sprite = change_img[6]; //도감 원래 이미지로 변경
                CenterFlame.instance.bgms[PlayerController.nStage].source.Play();
            }
        }
        
    }
    public void change(int n) {
        Destroy(cover[n]); //n번째 활성화 (0번째부터 생각해야 댐)
    }
    public void Button1() {
        image.sprite = change_img[0]; //1번 아이템 도감 이미지로 변경
    }
    public void Button2() {
        image.sprite = change_img[1];
    }
    public void Button3() {
        image.sprite = change_img[2];
    }
    public void Button4() {
        image.sprite = change_img[3];
    }
    public void Button5() {
        image.sprite = change_img[4];
    }
    public void Button6() {
        image.sprite = change_img[5];
    }
}
