using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
    Tab theTab;
    public Image[] DogamArr;
    string[] collection = new string[6];
    public Text m_TypingText; 
    public Text CollectT;
    public Text SheetT;
    public string m_Message;     
    public float m_Speed; 
    public Button ToMain;
    public Button Replay;
    // Start is called before the first frame update
    void Start() 
    { 
        collection[1] = "마녀의 수정 구슬을(를) 얻었다!";
        collection[2] = "알 수 없는 힘이 담긴 듯한 목걸이을(를) 얻었다!";
        collection[3] = "왕가의 반지을(를) 얻었다!";
        collection[4] = "심해 상어의 이빨을(를) 얻었다!";
        collection[5] = "악보 조각을(를) 얻었다!";
        theTab = FindObjectOfType<Tab>();
        if (SceneManager.GetActiveScene().name == "NormalEnding")
            m_Message = @"모든 적과 싸워 이겼지만 출구는 어디에도 보이지 않았다...";
        else 
            m_Message = @"적과 싸워서 이기지 못했다...";
        StartCoroutine(typing(m_TypingText, m_Message, m_Speed)); 
    } 

    IEnumerator typing(Text typingText, string message, float speed) 
    { 
        for (int i = 0; i < message.Length; i++) 
        { 
            typingText.text = message.Substring(0, i + 1); 
            yield return new WaitForSeconds(speed); 
        }
        if (SceneManager.GetActiveScene().name == "NormalEnding") {
            if (theTab.collectionData.collect[Tab.nstage] == false) {
                StartCoroutine(Dogam(Tab.nstage, m_TypingText, DogamArr));
                StartCoroutine(Sheet(5, CollectT, DogamArr));
            }
        }
        Invoke(nameof(Show), 5.0f);
    } 
    void Show() {
        ToMain.gameObject.SetActive(true);
        Replay.gameObject.SetActive(true);
        theTab.LoadCollectionDataToJson();
    }
    
    //
    IEnumerator Dogam(int n, Text t, Image[] arr) {
        yield return new WaitForSeconds(0.5f);
        t.gameObject.SetActive(false);
        arr[n].gameObject.SetActive(true);
        theTab.collectionData.collect[n] = true;
        StartCoroutine(typing(CollectT, collection[n], m_Speed));
    }
    IEnumerator Sheet(int n, Text t, Image[] arr) {
        yield return new WaitForSeconds(3.0f);
        t.gameObject.SetActive(false);
        arr[n].gameObject.SetActive(true);
        theTab.collectionData.collect[n] = true;
        StartCoroutine(typing(SheetT, collection[n], m_Speed));
    }
}
