using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static List<GameObject> networkJarheads;

    public int currPopulation;
    private int prevPopulation;

    // Start is called before the first frame update
    void Start()
    {
        networkJarheads = new List<GameObject>();
        currPopulation = networkJarheads.Count;
    }

    // Update is called once per frame
    void Update()
    {
        noteChanges();
        if (currPopulation > prevPopulation) Debug.Log("Added to network!");
    }

    // Notes previous and current observations
    void noteChanges()
    {
        prevPopulation = currPopulation;
        currPopulation = networkJarheads.Count;
    }
}
