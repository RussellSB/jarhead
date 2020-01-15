using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class Volume : MonoBehaviour
{
    public GameObject sliderUI;

    public void Start()
    {
        AudioListener.volume = AudioListener.volume;
        sliderUI.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
    }

    public void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }

    public void SetGameVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
