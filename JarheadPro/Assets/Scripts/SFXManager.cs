﻿using UnityEngine;
using System;

public class SFXManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void StepHeavy()
    {
        float random = UnityEngine.Random.Range(0.5f, 1f);
        Sound s = Array.Find(sounds, sound => sound.name == "Step");
        s.source.volume = 0.5f;
        s.source.pitch = random;
        s.source.Play();
    }

    public void StepLight()
    {
        float random = UnityEngine.Random.Range(2.5f, 3f);
        Sound s = Array.Find(sounds, sound => sound.name == "Step");
        s.source.volume = 0.1f;
        s.source.pitch = random;
        s.source.Play();
    }

    //GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Click");
}
