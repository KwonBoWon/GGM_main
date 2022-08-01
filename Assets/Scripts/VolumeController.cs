using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider Volume;
    void Update()
    {
     AudioListener.volume = Volume.value; //슬라이더로 볼륨조절
    }
 
 




}
