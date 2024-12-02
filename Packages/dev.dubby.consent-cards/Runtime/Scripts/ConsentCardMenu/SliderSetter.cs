
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Wrapper.Modules;

public class SliderSetter : UdonSharpBehaviour
{
    public TextMeshProUGUI tmp;
    
    public Slider slider;

    public void Start()
    {
        tmp.text = Math.Round(slider.value, 2).ToString();
    }


    public void SetText()
    {
        tmp.text = Math.Round(slider.value, 2).ToString();
    }
}
