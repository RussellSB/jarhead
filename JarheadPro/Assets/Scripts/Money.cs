using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    private float money = 1000f;
    public Text moneyText;
    private Sanitybar sanitybar;

    private void Awake()
    {
        // Retrieves the sanity bar. For testing purposes.
        sanitybar = transform.parent.parent.Find("SanityBar").GetComponent<Sanitybar>();
    }

    void Update()
    {
        // For testing purposes
        money -= (money / 1000);
        if (money < 0)
        {
            money = 0;
        }

        // Updates the decay of sanity.
        sanitybar.getSanity().setDecay((1 - (money / 1000f)) * 5);

        // Formats the text output.
        moneyText.text = string.Format("{0:0.00}", money);
    }

    private void Change(float amount)
    {
        money += amount;
    }
}
