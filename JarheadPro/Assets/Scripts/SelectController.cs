using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SelectController : MonoBehaviour
{
    public Texture2D jarbudCursor;
    public Texture2D jarheadCursor;

    public CursorMode cursorMode;
    public Vector2 hotSpot;

    private GameObject hitObject;

    private void Start()
    {
        cursorMode = CursorMode.Auto;
        hotSpot = new Vector2(jarbudCursor.width / 2, jarbudCursor.height / 2);
    }

    private void Update()
    {
        if (PauseMenu.isPaused)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            return;
        }

        Vector3 start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(start, transform.forward, out hit))
        {
            hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Jarbud")
            {
                Cursor.SetCursor(jarbudCursor, hotSpot, cursorMode);

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject infoBubble = hitObject.transform.Find("infoBubble").gameObject;
                    Animator animator = infoBubble.GetComponent<Animator>();
                    if (animator.GetBool("Close"))
                    {
                        animator.SetBool("Close", false);
                        animator.SetBool("Open", true);
                    } 
                    else
                    {
                        animator.SetBool("Open", false);
                        animator.SetBool("Close", true);
                    }
                }

                
            }
            if (hitObject.tag == "Jarhead")
            {
                Cursor.SetCursor(jarheadCursor, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    NetworkController.networkJarheads.Add(hitObject);
                    //hitObject.SetActive(false); //deactivates visibility
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}