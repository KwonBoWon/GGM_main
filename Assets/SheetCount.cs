using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetCount : MonoBehaviour
{
    // Start is called before the first frame update
    Tab theTab;
    public Text MusicCnt;
    public Text PuzzleCnt;
    public int Sheet;

    void Start()
    {
        theTab = FindObjectOfType<Tab>();
    }

    // Update is called once per frame
    void Update()
    {
        MusicCount();
        PuzzleCount();
    }

    void MusicCount(){
        Sheet = theTab.collectionData.SheetMusic;
        MusicCnt.text = Sheet.ToString();
    }
    void PuzzleCount() {
        PuzzleCnt.text = theTab.collectionData.puzzle.ToString();
    }
}
