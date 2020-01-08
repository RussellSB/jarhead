using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

// Class that controls the sanity bar.
// Also owns the Sanity object that should be used ingame.
public class Sanitybar : MonoBehaviour
{
    private Image barImage;
    private Image emoticon;
    private Sanity sanity;
    private Sprite great;
    private Sprite happy;
    private Sprite meh;
    private Sprite sad;
    private Sprite terrible;

    private void Awake()
    {
        // Images
        barImage = transform.Find("BarGroup").Find("bar").GetComponent<Image>();
        emoticon = transform.Find("Emoticon").Find("emoticon").GetComponent<Image>();

        // Loads up the sprites
        great = Resources.Load("great1", typeof(Sprite)) as Sprite;
        happy = Resources.Load("happy1", typeof(Sprite)) as Sprite;
        meh = Resources.Load("meh1", typeof(Sprite)) as Sprite;
        sad = Resources.Load("sad1", typeof(Sprite)) as Sprite;
        terrible = Resources.Load("terrible1", typeof(Sprite)) as Sprite;

        // Initialising the sanity
        sanity = new Sanity();
    }

    public Sanity getSanity()
    {
        return sanity;
    }

    private void Update()
    {
        sanity.Tick();
        setEmoticonAccordingToSanity();
        barImage.fillAmount = sanity.GetSanityNormalized();
    }

    private void setEmoticonAccordingToSanity()
    {
        float s = sanity.GetSanityNormalized();

        if(s > .8)
        {
            emoticon.sprite = great;
            barImage.color = Color.cyan;
        }
        else if(s > .6)
        {
            emoticon.sprite = happy;
            barImage.color = Color.green;
        }
        else if(s > .4)
        {
            emoticon.sprite = meh;
            barImage.color = Color.yellow;
        }
        else if(s > .2)
        {
            emoticon.sprite = sad;
            barImage.color = new Color(1.0f, 0.64f, 0.0f); // orange
        }
        else
        {
            emoticon.sprite = terrible;
            barImage.color = Color.red;
        }
    }
}

// Class that represents the sanity meter.
// This one represents the "decrease-over-time" version.
public class Sanity
{
    // Maximum value for sanity.
    public const int SANITY_MAX = 100;
    public const int SANITY_MIN = 0;
    private static float sanity = SANITY_MAX;
    private static float decay = 1f;

    // Updates sanity per tick safely.
    public void Tick()
    {
        float amount = decay * Time.deltaTime;
        if (sanity - amount < SANITY_MIN)
        {
            sanity = SANITY_MIN;
        }
        else
        {
            sanity -= amount;
        }
    }

    // Sets the sanity decay.
    public void setDecay(float decay)
    {
        if(decay >= 0 && decay <= 100)
        {
            Sanity.decay = decay;
        } 
    }

    // Updates sanity per the value passed safely.
    // Returns the updated sanity.
    public float Update(float value)
    {
        if (sanity + value > SANITY_MAX)
        {
            sanity = SANITY_MAX;
        }
        else if (sanity + value < SANITY_MIN)
        {
            sanity = SANITY_MIN;
        }
        else
        {
            sanity += value;
        }

        return sanity;
    }

    // Gets the normalized sanity
    public float GetSanityNormalized()
    {
        return sanity / (SANITY_MAX + Math.Abs(SANITY_MIN));
    }
}

// Class that represents the sanity meter.
// This one is the same as Sanity, but without the "decrease-over-time" functionality.
public class SanityLegacy
{
    // Maximum value for sanity.
    public const int SANITY_MAX = 100;
    public const int SANITY_MIN = 0;
    private float sanity;

    public SanityLegacy()
    {
        sanity = SANITY_MAX;
    }

    // Updates sanity safely, keeping it within the bounds.
    // Also returns the new sanity value.
    public float Update(float value)
    {
        if(sanity + value > SANITY_MAX)
        {
            sanity = SANITY_MAX;
        }
        else if(sanity + value < SANITY_MIN)
        {
            sanity = SANITY_MIN;
        }
        else
        {
            sanity += value;
        }

        return sanity;
    }

    // Gets the normalized sanity
    public float GetSanityNormalized()
    {
        return sanity / (SANITY_MAX + Math.Abs(SANITY_MIN));
    }
}
