using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null; //애니메이터 변수
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;//perfect애니매이션
    [SerializeField] UnityEngine.UI.Image judgementImage = null;//이미지 변수 
    [SerializeField] Sprite[] judgementSprite = null; //판정 스프라이트 변수 배열

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
