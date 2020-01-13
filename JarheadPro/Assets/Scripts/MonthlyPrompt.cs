using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthlyPrompt : MonoBehaviour
{
    public GameObject monthlyPromptUI;
    public GameObject intervalController;

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
        gameObject.GetComponent<PauseMenu>().enabled = true;
        intervalController.GetComponent<IntervalController>().newInterval();
        monthlyPromptUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Popup()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayerController.velocity = Vector3.zero;
        monthlyPromptUI.SetActive(true);
        gameObject.GetComponent<PauseMenu>().enabled = false;
        Time.timeScale = 0f;
    }
}
