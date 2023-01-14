using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour
{
    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject LoadingCanvas;
   public void Play() {
        //여기다가 그냥 ui넣어서 셋액티브 가자고
        MainCanvas.SetActive(false);

        SceneManager.LoadScene("Note");
   }
}
