using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Note : MonoBehaviour
{
    public float noteSpeed = 400;//��Ʈ�� �ӵ�
    UnityEngine.UI.Image noteImage;

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
        //ó���� ��Ʈ�� ��ƾ� �뷡�� ����ǹǷ� �̹����� ������
    }
    public bool GetNoteFlag()//��Ʈ �̹����� ���������� ��ȯ
    {
        return noteImage.enabled;
    }

}
