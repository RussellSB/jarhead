using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SelectController : MonoBehaviour
{
    public Texture2D jarbudCursor;
    public Texture2D jarheadCursor;

    public CursorMode cursorMode;
    public Vector2 hotSpot;

    public GameObject Input_partyController;
    public GameObject Input_networkController;
    private PartyController partyController;
    private NetworkController networkController;

    private GameObject hitObject;

    private void Start()
    {
        cursorMode = CursorMode.Auto;
        hotSpot = new Vector2(jarbudCursor.width / 2, jarbudCursor.height / 2);

        partyController = Input_partyController.GetComponent<PartyController>();
        networkController = Input_networkController.GetComponent<NetworkController>();
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
            if (hitObject.tag == "Jarbud" && !partyController.partyJarbuds.Contains(hitObject))
            {
                Cursor.SetCursor(jarbudCursor, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    hitObject.GetComponent<Scrolling>().enabled = false;
                    partyController.partyJarbuds.Add(hitObject);
                }
            }
            if (hitObject.tag == "Jarhead")
            {
                Cursor.SetCursor(jarheadCursor, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    networkController.networkJarheads.Add(hitObject);
                    hitObject.SetActive(false); //deactivates visibility
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}