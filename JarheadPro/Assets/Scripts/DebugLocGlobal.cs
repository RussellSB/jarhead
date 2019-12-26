using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLocGlobal : MonoBehaviour
{
    /* Y-to-Z fixed correlation mapping for colliders
    Y       Z
    -17     1.9
    -18     1
    -19     0
    -20     -0.9
    -21     -1.9        
    */

    // UPPER BOUND: x, -17, 1.9
    // LOWER BOUND: x, -21, -1.9

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
    }
}
