using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionPrompt : MonoBehaviour
{
    public GameObject decisionPromptUI;

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

    public void Popup()
    {
        gameObject.GetComponent<PauseMenu>().enabled = false;
        decisionPromptUI.SetActive(true);
        PauseMenu.isPaused = true;
        Time.timeScale = 0f;
    }
}
