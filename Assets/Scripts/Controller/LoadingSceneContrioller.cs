using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneContrioller : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image prograssBar;
    public static void LoadScnene(string sceneName){
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess(){//코루틴
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//비동기씬이동
        op.allowSceneActivation = false;//90% 로드하고 기다림 true하면 완료하고 넘어감(페이크로딩)

        float timer = 0f;
        while(!op.isDone){
            yield return null;//반복문이 돌때마다 제어권을 넘김

            if(op.progress <0.9f){
                prograssBar.fillAmount = op.progress;
            }
            else{
                timer += Time.unscaledDeltaTime;
                prograssBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(prograssBar.fillAmount >= 1f){
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
