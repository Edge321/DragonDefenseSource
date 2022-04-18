using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehavior : MonoBehaviour
{
    public Text sliderText;

    private Slider slider;

    private float volume;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        Volume();
        SetVolumeText();
    }
    /// <summary>
    /// Sets the master volume of the game
    /// </summary>
    private void Volume()
    {
        volume = slider.value / 100.0f;
        AudioBehavior.instance.SetVolume(volume);
    }
    /// <summary>
    /// Sets the volume text in the volume canvas
    /// </summary>
    private void SetVolumeText()
    {
        sliderText.text = "Master Volume: " + slider.value.ToString("F0");
    } 
}
