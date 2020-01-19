using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoOpenClose : MonoBehaviour
{
    public void Open()
    {
        FindObjectOfType<SFXManager>().Open();
    }

    public void Close()
    {
        FindObjectOfType<SFXManager>().Close();
    }
}
