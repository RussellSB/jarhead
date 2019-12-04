using UnityEngine;
using System.Collections;

public class CursorChange : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void FixedUpdate()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}