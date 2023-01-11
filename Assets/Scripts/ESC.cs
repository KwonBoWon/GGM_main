using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ESC : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image; 
    public Slider Volume;
    public GameObject soundButtonON;
    public GameObject soundButtonOFF;  
    public GameObject ExitButton; 
    public static int ESCck = 1;
    public  static bool gameSound = true;
    Image BtnOnImage;
    Image BtnOffImage;
   Color BtnOnColor;
   Color BtnOffColor;
    public ESC()
    {
        ESCck = 1;
    }
    void Start()
    {
        this.image = GetComponent<Image>();
        soundButtonON.SetActive(false);
        soundButtonOFF.SetActive(false);

        BtnOnImage = soundButtonON.GetComponent<Image>();
        BtnOnColor = BtnOnImage.color;
        BtnOnColor.a = 0.0f;
        BtnOnImage.color = BtnOnColor;

        BtnOffImage = soundButtonOFF.GetComponent<Image>();
        BtnOffColor = BtnOffImage.color;
        BtnOffColor.a = 0.5f;
        BtnOffImage.color = BtnOffColor;
    }
    public void GameSoundON()
    {
        gameSound = true;
        BtnOnColor.a = 0.0f;
        BtnOnImage.color = BtnOnColor;

        BtnOffColor.a = 0.5f;
        BtnOffImage.color = BtnOffColor;

    }
    public void GameSoundOFF()
    {
        gameSound = false;
        BtnOnColor.a = 0.5f;
        BtnOnImage.color = BtnOnColor;

        BtnOffColor.a = 0.0f;
        BtnOffImage.color = BtnOffColor;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ESCck == 1) { //esc키 누르면 보이게
            soundButtonON.SetActive(true);
            soundButtonOFF.SetActive(true);
            this.image.enabled = true;
            Time.timeScale = 0.0F;
            if (SceneManager.GetActiveScene().name == "Note") {
                CenterFlame.instance.bgms[PlayerController.nStage].source.Pause();
                if(gameSound)SoundEffectManager.instance.Sounds[0].source.Play();
            }
            ExitButton.gameObject.SetActive(true);
            ESCck = 0;
            Volume.gameObject.SetActive(true);
            
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ESCck == 0) { //esc키 또 누르면 없어지게
            soundButtonON.SetActive(false);
            soundButtonOFF.SetActive(false);
            this.image.enabled = false;
            ESCck = 1;
            if (Tab.tabck != 0) {
                Time.timeScale = 1.0F;
                if (SceneManager.GetActiveScene().name == "Note")
                    CenterFlame.instance.bgms[PlayerController.nStage].source.Play();
            }
            Volume.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(false);
        }
    }
}
