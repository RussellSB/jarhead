using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverUI;
    private bool triggered = false;
    public TextMeshProUGUI warning;

    private void Awake()
    {
        Statics.ResetStatics();
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered & Money.money <= 0)
        {
            triggered = true;
            warning.text = "You lost all your money and failed to meet your monthly expenses.";
            PopUp();
        }

        if(!triggered & Sanity.sanity <= 0)
        {
            triggered = true;
            warning.text = "Your sanity dropped too low. You lost all motivation to continue.";
            PopUp();
        }
    }

    void PopUp()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().mute = true;
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Gameover");

        PlayerController.velocity = Vector3.zero;
        gameoverUI.SetActive(true);
        PauseMenu.isPaused = true;
        gameObject.GetComponent<PauseMenu>().enabled = false;
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Restart()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
