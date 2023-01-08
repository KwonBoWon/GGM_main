using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null; //�ִϸ����� ����
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;//perfect�ִϸ��̼�
    [SerializeField] UnityEngine.UI.Image judgementImage = null;//�̹��� ���� �迭
    [SerializeField] Sprite[] judgementSprite = null; //���� ��������Ʈ ���� �迭

    public void JudgementEffect(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
