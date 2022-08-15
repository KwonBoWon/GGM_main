using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class Tab : MonoBehaviour
{
    public SpriteRenderer image; //도감창 렌더러를 받음
    public Sprite[] change_img; //변경할 이미지
    public Image[] cover; //수집 안 된 애들 비활성화시키는 이미지
    public static int tabck = 1;
    public Slider MonsterHP;

        public CollectionData collectionData;
    [ContextMenu("To Json Data")]
    void SaveCollectionDataToJson()//데이터 저장
    {
        string jsonData = JsonUtility.ToJson(collectionData, true);
        string path = Path.Combine(Application.dataPath, "collectionData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadCollectionDataToJson()//데이터 로드
    {
        string path = Path.Combine(Application.dataPath, "collectionData.json");
        string jsonData = File.ReadAllText(path);
        collectionData= JsonUtility.FromJson<CollectionData>(jsonData);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "note")
            MonsterHP = GameObject.Find("MonsterHP").GetComponent<Slider>();
        LoadCollectionDataToJson(); //데이터 로드
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "note") {
            if (Input.GetKeyDown(KeyCode.Tab)) { //탭키 누르는 순간 보이게
                MonsterHP.gameObject.SetActive(false);

         		SoundEffectManager.instance.Sounds[0].source.Play();
      			CenterFlame.instance.bgms[PlayerController.nStage].source.Pause();
                image.sprite = change_img[6];
                image.enabled = true;
                foreach (Image black in cover) {
                    black.enabled = true; //비활성화 시키는 애들
                }
                Time.timeScale = 0.0F;
                tabck = 0; 
            }
            if (Input.GetKeyUp(KeyCode.Tab)) { //탭키 떼는 순간 안 보이게
                MonsterHP.gameObject.SetActive(true);
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
        else {
            foreach (Image black in cover) {
                    black.enabled = true; //비활성화 시키는 애들
            }
            if (Input.GetKeyDown(KeyCode.Tab))
                SceneManager.LoadScene("Start");
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



[System.Serializable]//데이터직렬화
public class CollectionData
{
    public bool[] collect = new bool[6];
}
