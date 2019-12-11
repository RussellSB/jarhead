using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    private int money = 100;
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money : €" + money;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            money--;
        }
    }
}
