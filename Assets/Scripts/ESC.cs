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
    

    public ESC()
    {
        ESCck = 1;
    }
    void Start()
    {
        this.image = GetComponent<Image>();
        soundButtonON.SetActive(false);
        soundButtonOFF.SetActive(false);
    }
    public void GameSoundON()
    {
        gameSound = true;

    }
    public void GameSoundOFF()
    {
        gameSound = false;

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
