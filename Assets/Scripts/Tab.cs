using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class Tab : MonoBehaviour
{
    string[] explain = new string[7];
    string[] Name = new string[7];
    [SerializeField] UnityEngine.UI.Text Explain;
    [SerializeField] UnityEngine.UI.Text itemName;
    [SerializeField] UnityEngine.UI.Text PuzzleCnt;
    [SerializeField] UnityEngine.UI.Text SheetCnt;
    public SpriteRenderer image; //도감창 렌더러를 받음
    public Sprite[] change_img; //변경할 이미지
    public Image[] cover; //수집 안 된 애들 비활성화시키는 이미지
    public static int tabck = 1;
    public Slider MonsterHP;
    public static int nstage;


    public CollectionData collectionData;

    Tab()
    {
        tabck = 1;


    }

    [ContextMenu("To Json Data")]
    public void SaveCollectionDataToJson()//데이터 저장
    {
        string jsonData = JsonUtility.ToJson(collectionData, true);
        string path = Path.Combine(Application.dataPath, "collectionData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadCollectionDataToJson()//데이터 로드
    {
        string path = Path.Combine(Application.dataPath, "collectionData.json");
        string jsonData = File.ReadAllText(path);
        collectionData= JsonUtility.FromJson<CollectionData>(jsonData);
    }
    // Start is called before the first frame update
    void Start()
    {
        explain[0] = "자주색 빛을 띠는 수정 구슬이다. 유리로 되어 있어 금방이라도 깨질 것 같지만, 떨어뜨려도 깨지지 않을 만큼 단단하다. 구슬에 귀를 대면 마녀의 기이한 웃음 소리가 들린다. 리듬에 맞춰 반짝이는 것을 보니 어딘가에서 마녀가 지켜 보고 있을지도 모른다는 생각이 든다.";
        explain[1] = "왕가에 대대로 전해져 내려오는 반지였다. 어느 왕녀가 도망친 이후로 행방이 묘연했으나 던전에서 발견되었다. 그 왕녀도 이 던전에 떨어졌던 걸까? 아직도 보석은 빛을 잃지 않고 있다.";
        explain[2] = "이 던전에 들어온 다른 용사가 남긴 것 같다. 진주와 보석으로 되어 있어 팔면 비싸게 팔릴 것 같다.";
        explain[3] = "깊은 바닷속 심해 상어의 이빨. 어째서 여기에 떨어져 있는지는 알 수 없다. 소문에 의하면 이 이빨의 주인은 온몸이 황금색이라고 한다. 어쩌면 이 이빨도 예전에는 황금색이었을지도...?";
        explain[4] = "오래된 노래가 적혀 있는 악보다. 어딘가 음산한 느낌이 든다. 이 던전을 탈출할 실마리가 될지도 모른다.";
        explain[5] = "독특한 무늬가 새겨진 퍼즐 조각이다. 정교하게 만들어진 무늬가 중간에 끊긴 것으로 보아 다 모으면 어떤 모양이 완성될 것 같다.";
        explain[6] = "퍼즐 네 조각이 모여 완성된 그림이다. 미로와 이상한 육각형 로고가 그려져 있다. 퍼즐의 뒷면엔 '판도라 큐브'라고 적혀 있다. 무슨 의미가 있는 걸까...?";
        Name[0] = "{ 마녀의 수정 구슬 }";
        Name[1] = "{ 왕가의 반지 }";
        Name[2] = "{ 용사의 유품 }";
        Name[3] = "{ 심해 상어의 이빨 }";
        Name[4] = "{ 악보 조각 }";
        Name[5] = "{ 퍼즐 조각 }";
        Name[6] = "{ 완성된 퍼즐 }";
        if (SceneManager.GetActiveScene().name == "Note")
            MonsterHP = GameObject.Find("MonsterHP").GetComponent<Slider>();
        LoadCollectionDataToJson(); //데이터 로드
        if (SceneManager.GetActiveScene().name == "Dogam") {
            for (int i = 1; i < 5; i++) {
                if (collectionData.collect[i] == false) {
                    Debug.Log(collectionData.collect[i]);
                    cover[i-1].enabled = true; //비활성화 시키는 애들
                }
            }
            if (collectionData.SheetMusic == 0) {
                cover[4].enabled = true;
            }
            if (collectionData.puzzle == 0)
                cover[5].enabled = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Note") {
            if (Input.GetKeyDown(KeyCode.Tab)) { //탭키 누르는 순간 보이게
                Explain.text = "";
                itemName.text = "";
                MonsterHP.gameObject.SetActive(false);
                PuzzleCnt.gameObject.SetActive(true);
                SheetCnt.gameObject.SetActive(true);
                Explain.gameObject.SetActive(true);
                itemName.gameObject.SetActive(true);
                if (ESC.gameSound) SoundEffectManager.instance.Sounds[0].source.Play();
      			CenterFlame.instance.bgms[PlayerController.nStage].source.Pause();
                image.sprite = change_img[6];
                image.enabled = true;
                for (int i = 1; i < 5; i++) {
                if (collectionData.collect[i] == false) {
                    cover[i-1].enabled = true; //비활성화 시키는 애들
                }
                }
                if (collectionData.SheetMusic == 0) {
                    cover[4].enabled = true;
                }
                if (collectionData.puzzle == 0)
                    cover[5].enabled = true;
                Time.timeScale = 0.0F;
                tabck = 0;
            }
            if (Input.GetKeyUp(KeyCode.Tab)) { //탭키 떼는 순간 안 보이게
                MonsterHP.gameObject.SetActive(true);
                PuzzleCnt.gameObject.SetActive(false);
                SheetCnt.gameObject.SetActive(false);
                Explain.gameObject.SetActive(false);
                itemName.gameObject.SetActive(false);
                image.enabled = false;
                tabck = 1;
                if (ESC.ESCck != 0) {
                    for (int i = 0; i < 6; i++)
                        cover[i].enabled = false; //비활성화 시키는 애들
                }
                    Time.timeScale = 1.0F;
                    image.sprite = change_img[6]; //도감 원래 이미지로 변경
                    CenterFlame.instance.bgms[PlayerController.nStage].source.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Dogam") {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                SceneManager.LoadScene("Start");
            }
        }

    }

    public void Button1() {
        image.sprite = change_img[0]; //1번 아이템 도감 이미지로 변경
        Explain.text = explain[0];
        itemName.text = Name[0];
    }
    public void Button2() {
        image.sprite = change_img[1];
        Explain.text = explain[1];
        itemName.text = Name[1];
    }
    public void Button3() {
        image.sprite = change_img[2];
        Explain.text = explain[2];
        itemName.text = Name[2];
    }
    public void Button4() {
        image.sprite = change_img[3];
        Explain.text = explain[3];
        itemName.text = Name[3];
    }
    public void Button5() {
        image.sprite = change_img[4];
        Explain.text = explain[4];
        itemName.text = Name[4];
    }
    public void Button6() {
        if (collectionData.puzzle == 4) {
            image.sprite = change_img[7];
            Explain.text = explain[6];
            itemName.text = Name[6];
        }
        else {
            image.sprite = change_img[5];
            Explain.text = explain[5];
            itemName.text = Name[5];
        }
    }
}





[System.Serializable]//데이터직렬화
public class CollectionData
{
    public bool[] collect = new bool[5]; // 스테이지 보스가 죽었는지 확인하는 용도
    public int[] Clear = new int[5]; // 스테이지 최종 보스 몇 번 죽였는지
    public int SheetMusic = 0; //악보 조각
    public int puzzle = 0;

}