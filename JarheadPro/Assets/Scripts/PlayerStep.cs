using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour
{
    void Step()
    {
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().StepHeavy();
    }
}
