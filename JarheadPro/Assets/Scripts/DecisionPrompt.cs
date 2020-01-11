using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionPrompt : MonoBehaviour
{
    public GameObject decisionPromptUI;

    // Update is called once per frame
    void Update()
    {
    }

    public void Option1()
    {
        gameObject.GetComponent<PauseMenu>().enabled = true;
        decisionPromptUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Option2()
    {
        gameObject.GetComponent<PauseMenu>().enabled = true;
        decisionPromptUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Popup()
    {
        gameObject.GetComponent<PauseMenu>().enabled = false;
        decisionPromptUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
