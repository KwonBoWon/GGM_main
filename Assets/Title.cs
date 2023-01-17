using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Title : MonoBehaviour
{
    Tab theTab;
    public static Image thisImg;
    public Sprite EnglishImage;
    public Sprite KoreanImage;
    // Start is called before the first frame update
    void Start()
    {
        thisImg = GetComponent<Image>();
        theTab = FindObjectOfType<Tab>();
        ChangeTitleLanguage(EnglishImage, KoreanImage, theTab.collectionData.Language);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ChangeTitleLanguage(Sprite English, Sprite Korean, int LanguageNumber) {
        if (LanguageNumber == 0)
            thisImg.sprite = English;
        else
            thisImg.sprite = Korean;
    }
}
