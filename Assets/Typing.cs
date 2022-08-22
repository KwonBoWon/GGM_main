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
    public GameObject White;
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
        collection[4] = "마녀의 수정 구슬을(를) 얻었다!";
        collection[2] = "알 수 없는 힘이 담긴 듯한 목걸이을(를) 얻었다!";
        collection[1] = "왕가의 반지을(를) 얻었다!";
        collection[3] = "심해 상어의 이빨을(를) 얻었다!";
        collection[5] = "악보 조각을(를) 얻었다!";
        collection[6] = "퍼즐 조각을(를) 얻었다!";
        theTab = FindObjectOfType<Tab>();
        if (SceneManager.GetActiveScene().name == "NormalEnding")
            m_Message = @"모든 적과 싸워 이겼지만 출구는 어디에도 보이지 않았다...";
        else if (SceneManager.GetActiveScene().name == "GameOver")
            m_Message = @"적과 싸워서 이기지 못했다...";
        StartCoroutine(typing(m_TypingText, m_Message, m_Speed)); 
        if (SceneManager.GetActiveScene().name == "NormalEnding") {
            if (theTab.collectionData.collect[Tab.nstage] == false) {
                StartCoroutine(Dogam(Tab.nstage, m_TypingText, DogamArr));
                StartCoroutine(Sheet(5, CollectT, DogamArr));
                
            }
            else if (theTab.collectionData.Clear[Tab.nstage] == 1) {
                StartCoroutine(Sheet(6, m_TypingText, DogamArr));
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
        ToMain.transform.SetAsLastSibling();
        Replay.transform.SetAsLastSibling();
        ToMain.gameObject.SetActive(true);
        Replay.gameObject.SetActive(true);
        theTab.LoadCollectionDataToJson();
    }
    
    //
    IEnumerator Dogam(int n, Text t, Image[] arr) {
        yield return new WaitForSeconds(4.0f);
        t.gameObject.SetActive(false);
        arr[n].gameObject.SetActive(true);
        theTab.collectionData.collect[n] = true;
        theTab.collectionData.Clear[n]++;
        theTab.SaveCollectionDataToJson();
        StartCoroutine(typing(CollectT, collection[n], m_Speed));
    }
    IEnumerator Sheet(int n, Text t, Image[] arr) {
        if (n == 5) {
            yield return new WaitForSeconds(7.0f);
            t.gameObject.SetActive(false);
            arr[Tab.nstage].gameObject.SetActive(false);
            arr[n].gameObject.SetActive(true);
            theTab.collectionData.SheetMusic++;
            StartCoroutine(typing(SheetT, collection[n], m_Speed));
            Invoke(nameof(Show), 3.0f);
        }
        else {
            yield return new WaitForSeconds(4.0f);
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
            StartCoroutine(Dogam(Tab.nstage, m_TypingText, DogamArr));
        }
        else if (theTab.collectionData.Clear[Tab.nstage] == 1) {
            StartCoroutine(Sheet(6, m_TypingText, DogamArr));
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
            White.transform.SetAsLastSibling();
            CreditText.transform.SetAsLastSibling();
            Credit.SetTrigger("hit");
            Invoke(nameof(JinEnding), 14.0f);
        }
        else {
            CutCnt++;
        }

    }
}


