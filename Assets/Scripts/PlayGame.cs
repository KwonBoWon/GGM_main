using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour
{
    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject LoadingCanvas;
   public void Play() {
        //����ٰ� �׳� ui�־ �¾�Ƽ�� ���ڰ�
        MainCanvas.SetActive(false);

        SceneManager.LoadScene("Note");
   }
}
