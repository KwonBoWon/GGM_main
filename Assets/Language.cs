using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    Title title;
    Tab theTab;
    public static int LanguageNumber = 0;
    // Start is called before the first frame update

    void Start()
    {
        //title = GameObject.Find("Title").GetComponent<Title>();
        title = FindObjectOfType<Title>();
        theTab = FindObjectOfType<Tab>();
        LanguageNumber = theTab.collectionData.Language;
        //Title.ChangeTitleLanguage(title.EnglishImage, title.KoreanImage);
    }

    // Update is called once per frame

    public void KoreanButton() {
        LanguageNumber = 1;
        Title.ChangeTitleLanguage(title.EnglishImage, title.KoreanImage, LanguageNumber);
        theTab.collectionData.Language = LanguageNumber;
        theTab.SaveCollectionDataToJson();
    }
    public void EnglishButton() {
        LanguageNumber = 0;
        Title.ChangeTitleLanguage(title.EnglishImage, title.KoreanImage, LanguageNumber);
        theTab.collectionData.Language = LanguageNumber;
        theTab.SaveCollectionDataToJson();
    }
}
