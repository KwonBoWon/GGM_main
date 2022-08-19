using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToStart : MonoBehaviour
{
   public void ToStartVoid() {
      Time.timeScale = 1.0F;
        SceneManager.LoadScene("Start");
   }
}
