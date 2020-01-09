using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Month : MonoBehaviour
{
    public Text month;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        month.text = string.Format("{0:0}", IntervalController.intervalCount);
    }
}
