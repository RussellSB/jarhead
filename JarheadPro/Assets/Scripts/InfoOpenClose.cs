using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoOpenClose : MonoBehaviour
{
    public void Open()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Open");
    }

    public void Close()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Close");
    }
}
