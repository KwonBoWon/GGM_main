 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
    Tab theTab;
    int CutCnt = 1;
    public Animator FadePannel;
    Animator ani;
    public GameObject Fade;
    public GameObject Cut1;
    public GameObject Cut2;
    public GameObject Cut3;
    public GameObject Cut4;
    public GameObject CreditText;
    public Image[] DogamArr;
    string[] collection = new string[7];
    public Animator Credit;
    public Text m_TypingText; 
    public Text CollectT;
    public Text SheetT;
    public Text PuzzleT;
    public string m_Message;     
    public float m_Speed; 
    public Button ToMain;
    public Button Replay;
    // Start is called before the first frame update
    void Start() 
    { 
        if (SceneManager.GetActiveScene().name == "JinEnding") {
            Cut4.transform.SetAsLastSibling();
            Cut3.transform.SetAsLastSibling();
            Cut2.transform.SetAsLastSibling();
            Cut1.transform.SetAsLastSibling();
            Fade.transform.SetAsLastSibling();
        }
        Time.timeScale = 1.0F;
        collection[1] = "왕가의 반지을(를) 얻었다!";
        collection[2] = "알 수 없는 힘이 담긴 듯한 목걸이을(를) 얻었다!";
        collection[3] = "심해 상어의 이빨을(를) 얻었다!";
        collection[4] = "마녀의 수정 구슬을(를) 얻었다!";
        collection[5] = "악보 조각을(를) 얻었다!";
        collection[6] = "퍼즐 조각을(를) 얻었다!";
        theTab = FindObjectOfType<Tab>();
        if (SceneManager.GetActiveScene().name == "NormalEnding")
            m_Message = @"모든 적과 싸워 이겼지만 출구는 어디에도 보이지 않았다...";
        else if (SceneManager.GetActiveScene().name == "GameOver")
            m_Message = @"적과 싸워서 이기지 못했다...";
        StartCoroutine(typing(m_TypingText, m_Message, m_Speed)); //
        if (SceneManager.GetActiveScene().name == "NormalEnding") { //노멀 엔딩이면
            if (theTab.collectionData.collect[Tab.nstage] == false) { //처음 깨는 거면 도감, 악보 조각 얻기
                StartCoroutine(Dogam(Tab.nstage, m_TypingText, DogamArr, m_Message));
                StartCoroutine(Sheet(5, CollectT, DogamArr, collection[Tab.nstage], m_Message));
                
            }
            else if (theTab.collectionData.Clear[Tab.nstage] == 1) { //이미 깬 스테이지면 악보 조각 얻기
                StartCoroutine(Sheet(6, m_TypingText, DogamArr, m_Message, ""));
            }
            else{
                Invoke(nameof(Show), 3.0f);
            }
        }
        else if (SceneManager.GetActiveScene().name == "JinEnding") { //진엔딩이라면
            ChangeScene();
            Invoke(nameof(ChangeScene), 5.0f);
            Invoke(nameof(ChangeScene), 10.0f);
            Invoke(nameof(ChangeScene), 15.0f);
        
        }
        else
            Invoke(nameof(Show), 3.0f);
    } 

    IEnumerator typing(Text typingText, string message, float speed) 
    { 
        for (int i = 0; i < message.Length; i++) 
        { 
            typingText.text = message.Substring(0, i + 1); 
            yield return new WaitForSeconds(speed); 
        }
    } 
    void Show() {
        // ToMain.transform.SetAsLastSibling();
        // Replay.transform.SetAsLastSibling();
        ToMain.gameObject.SetActive(true);
        Replay.gameObject.SetActive(true);
        theTab.LoadCollectionDataToJson();
    }
    
    //
    IEnumerator Dogam(int n, Text t, Image[] arr, string str) {
        Debug.Log(str.Length);
        yield return new WaitForSeconds(str.Length * m_Speed + 2);
        t.gameObject.SetActive(false); //이전 타이핑 오브젝트 지워
        arr[n].gameObject.SetActive(true); //도감 사진 띄워
        theTab.collectionData.collect[n] = true; //얻은 걸로 기록
        theTab.collectionData.Clear[n]++; //한 번 깼다고 기록
        theTab.SaveCollectionDataToJson(); //데이터 저장
        StartCoroutine(typing(CollectT, collection[n], m_Speed)); //아이템 얻었다고 타이핑 해
    }
    IEnumerator Sheet(int n, Text t, Image[] arr, string str, string str2) {
        if (n == 5) {
            yield return new WaitForSeconds(str.Length * m_Speed + 2 + str2.Length * m_Speed + 2); //7초 기다려
            t.gameObject.SetActive(false);  //이전 타이핑 오브젝트 지워
            arr[Tab.nstage].gameObject.SetActive(false); //도감 사진 지워
            arr[n].gameObject.SetActive(true); //악보 사진 띄워
            theTab.collectionData.SheetMusic++; //얻은 걸로 기록
            StartCoroutine(typing(SheetT, collection[n], m_Speed)); //악보 얻었다고 타이핑 해
            Invoke(nameof(Show), 3.0f); //3초 후에 show 실행 (나가기 버튼 보이게)
        }
        else {
            yield return new WaitForSeconds(str.Length * m_Speed + 2 + str2.Length * m_Speed + 2);
            t.gameObject.SetActive(false);
            arr[n].gameObject.SetActive(true);
            theTab.collectionData.puzzle++;
            theTab.collectionData.Clear[Tab.nstage] += 1;
            StartCoroutine(typing(PuzzleT, collection[n], m_Speed));
            Invoke(nameof(Show), 3.0f);
        }
        theTab.SaveCollectionDataToJson();
    }
    void JinEnding() {
        if (theTab.collectionData.collect[Tab.nstage] == false) {
            StartCoroutine(Dogam(Tab.nstage, m_TypingText, DogamArr, ""));
            Invoke(nameof(Show), collection[Tab.nstage].Length * m_Speed + 3.0f);
        }
        else if (theTab.collectionData.Clear[Tab.nstage] == 1) {
            StartCoroutine(Sheet(6, m_TypingText, DogamArr, "", ""));
        }
        else{
            Invoke(nameof(Show), 3.0f);
        }
    }
    void ChangeScene() {
        FadePannel.SetTrigger("FadeIn");
        Invoke(nameof(Play), 0.3f);
        Invoke(nameof(Out), 3.0f);
        Invoke(nameof(False), 5.0f);

    }
    void Play() {
        if (CutCnt == 1) {
            ani = Cut1.GetComponent<Animator>();
            ani.SetTrigger("hit");
        }
        else if (CutCnt == 2) {
            ani = Cut2.GetComponent<Animator>();
            ani.SetTrigger("hit");
        }
    }
    void Out() {
        FadePannel.SetTrigger("FadeOut");
    }
    void False() {
        GameObject.Find("Cut" + CutCnt).gameObject.SetActive(false);
        if (CutCnt == 4){
            GameObject.Find("FadePannel").gameObject.SetActive(false);
            CreditText.transform.SetAsLastSibling();
            Credit.SetTrigger("hit");
            Invoke(nameof(JinEnding), 18.0f);
        }
        else {
            CutCnt++;
        }

    }
}


