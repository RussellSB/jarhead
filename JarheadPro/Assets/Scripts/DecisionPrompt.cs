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
    private string option1ID;
    private string option2ID;

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
        Chosen();
        EffectController.addEffect(option1ID);
    }

    public void option2()
    {
        Chosen();
        EffectController.addEffect(option2ID);
    }

    private void Chosen()
    {
        decisionPromptUI.SetActive(false);
        PauseMenu.isPaused = false;
        gameObject.GetComponent<PauseMenu>().enabled = true;
        isPrompted = false;
        Time.timeScale = 1f;
    }

    public void Popup(string type, DecisionScenario scenario)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        typeUI.text = type;
        descriptionUI.text = scenario.getDecision();
        option1UI.text = scenario.getOption1();
        option2UI.text = scenario.getOption2();

        option1ID = scenario.option1ID;
        option2ID = scenario.option2ID;

        decisionPromptUI.SetActive(true);
        isPrompted = true;

        PauseMenu.isPaused = true;
        gameObject.GetComponent<PauseMenu>().enabled = false;
        Time.timeScale = 0f;
    }
}
