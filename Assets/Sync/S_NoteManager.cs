using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear  = null; //��Ʈ�� �����Ǵ°�
    [SerializeField] GameObject goNote = null; //��Ʈ ������

    S_TimingManager theTimingManager;
    S_EffectManager theEffectManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<S_EffectManager>();
        theTimingManager = GetComponent<S_TimingManager>();
        //������Ʈ���� ������Ʈ�� ������(���⼱ TimingManager ��ũ��Ʈ)
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
            //60/bpm���� ��Ʈ ����
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm;//-�����ʰ� 0���μ����ϸ� ������ ����
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {//�̹����� ��������
                theEffectManager.JudgementEffect(4);//Miss�ִϸ��̼�
            }
            //��Ʈ�� �� ������ ���� ����
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }



}
