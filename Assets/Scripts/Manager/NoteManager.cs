using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;
    int arrowDirection =0;

    [SerializeField] Transform tfNoteAppear  = null; //��Ʈ�� �����Ǵ°�
    [SerializeField] GameObject[] goNote = null; //��Ʈ �������

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        //������Ʈ���� ������Ʈ�� ������(���⼱ TimingManager ��ũ��Ʈ)
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)  //60/bpm���� ��Ʈ ����
        {
            arrowDirection = Random.Range(0, 4); // �������� ���� ����
            GameObject t_note = Instantiate(goNote[arrowDirection], tfNoteAppear.position, Quaternion.identity); // ��Ʈ�� ����
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note); // ����Ʈ�� �߰�
            currentTime -= 60d / bpm; //-�����ʰ� 0���μ����ϸ� ������ ����
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Note")) 
        {
         
            /*
            if (collision.GetComponent<Note>().GetNoteFlag//�̹����� ��������
            {
                theEffectManager.JudgementEffect(4);//��Ʈ �������� MISS
            }
          */

            //��Ʈ�� �� ������ ���� ����
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }



}
