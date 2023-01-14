using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    Title title;
    public static int LanguageNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        title = GameObject.Find("Title").GetComponent<Title>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KoreanButton() {
        LanguageNumber = 1;
        Title.ChangeTitleLanguage(title.EnglishImage, title.KoreanImage);
    }
    public void EnglishButton() {
        LanguageNumber = 0;
        Title.ChangeTitleLanguage(title.EnglishImage, title.KoreanImage);
    }
}
