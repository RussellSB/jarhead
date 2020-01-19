using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarbudStep : MonoBehaviour
{
    void Step()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().StepLight();
    }
}
