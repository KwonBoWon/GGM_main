using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class soundController : MonoBehaviour
{
    public Slider audioSlider;
    void Update()
    {
     AudioListener.volume = audioSlider.value;
    }
 
 




}
