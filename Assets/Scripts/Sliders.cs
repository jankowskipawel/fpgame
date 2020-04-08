using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{

    public Slider soundSlider;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivitySliderText;
    public Slider FOVSlider;
    public TextMeshProUGUI FOVSliderText;
    public AudioMixer mixer;
    private MouseLook mouseLook;
    private Camera mainCamera;
    
    void Start()
    {
        soundSlider.onValueChanged.AddListener(delegate {SoundSliderValueChange();});
        FOVSlider.onValueChanged.AddListener(delegate {FOVSliderValueChange();});
        sensitivitySlider.onValueChanged.AddListener(delegate {SensitivitySliderValueChange();});
        mouseLook = GameObject.FindObjectOfType<MouseLook>();
        mainCamera = Camera.main;
    }

    void Update()
    {
       
    }

    public void SoundSliderValueChange()
    {
        mixer.SetFloat("Volume", soundSlider.value);
    }
    
    public void SensitivitySliderValueChange()
    {
        var tmp = sensitivitySlider.value;
        mouseLook.mouseSensitivity = tmp;
        sensitivitySliderText.text = $"{Math.Round(tmp/200, 2)}";
    }
    
    public void FOVSliderValueChange()
    {
        var tmp = FOVSlider.value;
        mainCamera.fieldOfView = tmp;
        FOVSliderText.text = $"{tmp}";
    }
    
}
