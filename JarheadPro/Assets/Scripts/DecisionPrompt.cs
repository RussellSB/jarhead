using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionPrompt : MonoBehaviour
{
    public GameObject decisionPromptUI;
    public Text typeUI;
    public Text descriptionUI;
    public Text option1UI;
    public Text option2UI;

    private bool isPrompted;

    public void Update()
    {
        if (isPrompted)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                option1();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                option2();
            }
        }
    }

    public void option1()
    {
        decisionPromptUI.SetActive(false);
        PauseMenu.isPaused = false;
        gameObject.GetComponent<PauseMenu>().enabled = true;
        isPrompted = false;
        Time.timeScale = 1f;
    }

    public void option2()
    {
        decisionPromptUI.SetActive(false);
        PauseMenu.isPaused = false;
        gameObject.GetComponent<PauseMenu>().enabled = true;
        isPrompted = false;
        Time.timeScale = 1f;
    }

    public void Popup(string type, string description, string option1, string option2)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        typeUI.text = type;
        descriptionUI.text = description;
        option1UI.text = option1;
        option2UI.text = option2;

        decisionPromptUI.SetActive(true);
        isPrompted = true;

        PauseMenu.isPaused = true;
        gameObject.GetComponent<PauseMenu>().enabled = false;
        Time.timeScale = 0f;
    }
}
