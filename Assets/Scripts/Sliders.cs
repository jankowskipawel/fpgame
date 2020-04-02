using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{

    public Slider soundSlider;
    public AudioMixer mixer;
    
    void Start()
    {
        soundSlider.onValueChanged.AddListener(delegate {SoundSliderValueChange();});
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SoundSliderValueChange()
    {
        mixer.SetFloat("Volume", soundSlider.value);
        Debug.Log(soundSlider.value);
    }
    
}
