using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionPrompt : MonoBehaviour
{
    public GameObject decisionPromptUI;
    public Text typeUI;

    public void option1()
    {
        gameObject.GetComponent<PauseMenu>().enabled = true;
        decisionPromptUI.SetActive(false);
        PauseMenu.isPaused = false;
        Time.timeScale = 1f;
    }

    public void option2()
    {
        gameObject.GetComponent<PauseMenu>().enabled = true;
        decisionPromptUI.SetActive(false);
        PauseMenu.isPaused = false;
        Time.timeScale = 1f;
    }

    public void Popup(string type)
    {
        gameObject.GetComponent<PauseMenu>().enabled = false;
        PauseMenu.isPaused = true;
        Time.timeScale = 0f;
        
        typeUI.text = type;
        decisionPromptUI.SetActive(true);
    }
}
