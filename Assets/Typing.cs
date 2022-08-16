using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public Text m_TypingText; 
    public string m_Message;     
    public float m_Speed = 0.5f; 
    public Button ToMain;
    public Button Replay;
    // Start is called before the first frame update
    void Start() 
    { 
        m_Message = @"모든 적과 싸워 이겼지만 출구는 어디에도 보이지 않았다..."; 

        StartCoroutine(typing(m_TypingText, m_Message, m_Speed)); 
    } 

    IEnumerator typing(Text typingText, string message, float speed) 
    { 
        for (int i = 0; i < message.Length; i++) 
        { 
            typingText.text = message.Substring(0, i + 1); 
            yield return new WaitForSeconds(speed); 
        } 
        Show();
    } 
    void Show() {
        ToMain.gameObject.SetActive(true);
        Replay.gameObject.SetActive(true);
    }
}