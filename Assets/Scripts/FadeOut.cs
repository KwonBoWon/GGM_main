using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Fade");
    }

    public IEnumerator Fade() {
        gameObject.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.008f) {
            Color c = gameObject.GetComponent<Image>().color;
            c.a = f;
            gameObject.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void Enable()//비활성화
    {
        gameObject.SetActive(false);

    }
    public void Able()//활성화
    {
        gameObject.SetActive(true);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
