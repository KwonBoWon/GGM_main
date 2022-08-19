using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static float noteSpeed = 400;//노트의  속도
    public int noteDirection; //노트 방향
    [Header("노트 종류 0:방어턴1:공격턴")]
    public int noteType = 0;


    UnityEngine.UI.Image noteImage;

    public Note()
    {
        noteSpeed = 400;
    }
    void Start()
    {
        noteImage = GetComponent<UnityEngine.UI.Image>();

    }


    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed*Time.deltaTime;
        
    }
    public void HideNote()
    {
        noteImage.enabled = false;
        //처음에 노트가 닿아야 노래가 실행되므로 이미지만 삭제함
    }
    public bool GetNoteFlag()//노트 이미지가 없어졌는지 반환
    {
        return noteImage.enabled;
    }

}
