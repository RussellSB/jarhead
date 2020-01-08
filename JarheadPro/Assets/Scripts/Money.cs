using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    public static float money = 1000f;
    public static float moneyOverTime = 0f;
    public Text moneyText;
    private Sanitybar sanitybar;

    private void Awake()
    {
        // Retrieves the sanity bar. For testing purposes.
        sanitybar = transform.parent.parent.Find("SanityBar").GetComponent<Sanitybar>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Update over time.
            money += moneyOverTime;
        }
        // Formats the text output.
        moneyText.text = string.Format("{0:0.00}", money);
    }
}
