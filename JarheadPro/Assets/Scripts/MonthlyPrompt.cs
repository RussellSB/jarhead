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
        PauseMenu.isPaused = false;
        Time.timeScale = 1f;
    }

    public void Popup()
    {
        gameObject.GetComponent<PauseMenu>().enabled = false;
        PlayerController.velocity = Vector3.zero;

        monthlyPromptUI.SetActive(true);
        PauseMenu.isPaused = true;
        Time.timeScale = 0f;
    }
}
