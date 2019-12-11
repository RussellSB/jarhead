using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{

    private int sanity = 100;
    public Text sanityText;

    // Update is called once per frame
    void Update()
    {
        sanityText.text = "Sanity : " + sanity;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sanity--;
        }
    }
}
