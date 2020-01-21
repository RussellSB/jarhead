using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonthlyPrompt : MonoBehaviour
{
    public GameObject monthlyPromptUI;
    public GameObject intervalController;

    public Text income;
    public Text expense;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && monthlyPromptUI.activeSelf)
        {
            Ok();
        }
    }

    public void Ok()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Click");
        gameObject.GetComponent<PauseMenu>().enabled = true;
        intervalController.GetComponent<IntervalController>().newInterval();
        EffectController.updateMoneyMonthly();
        monthlyPromptUI.SetActive(false);
        PauseMenu.isPaused = false;
        Time.timeScale = 1f;
    }

    public void Popup()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("MonthlyPrompt");

        income.text = string.Format("Income: €{0:0.00}", IntervalController.income);
        expense.text = string.Format("Expense: €{0:0.00}", IntervalController.expense);

        PlayerController.velocity = Vector3.zero;
        monthlyPromptUI.SetActive(true);
        PauseMenu.isPaused = true;
        gameObject.GetComponent<PauseMenu>().enabled = false;
        Time.timeScale = 0f;
    }
}
