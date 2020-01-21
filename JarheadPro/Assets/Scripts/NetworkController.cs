using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static List<GameObject> networkJarheads;

    public int currPopulation;
    private int prevPopulation;
    
    public static bool decided_partner = false;
    public static bool decided_child = false;
    public GameObject PartnerObj;
    public GameObject ChildObj;

    public GameObject intervalController;

    // Start is called before the first frame update
    void Start()
    {
        networkJarheads = new List<GameObject>();
        currPopulation = networkJarheads.Count;

        decided_partner = false;
        decided_child = false;
    }

    // Update is called once per frame
    void Update()
    {
        noteChanges();
        if (currPopulation > prevPopulation)
        {
            decideNetwork();
        } 
    }

    void decideNetwork()
    {
        bool decide_partner = networkJarheads[networkJarheads.Count - 1].name.Contains("Partner") && !PartnerObj;
        bool decide_child = networkJarheads[networkJarheads.Count - 1].name.Contains("Child") && !ChildObj;

        if (decide_child)
        {
            decided_child = true;
            ChildObj = networkJarheads[networkJarheads.Count - 1];
            EffectController.addEffect("JarheadChild");
            intervalController.GetComponent<IntervalController>().activateChild(); // activate child
        }

        if (decide_partner)
        {
            decided_partner = true;
            PartnerObj = networkJarheads[networkJarheads.Count - 1];
            EffectController.addEffect("JarheadPartner");
            intervalController.GetComponent<IntervalController>().activatePartner(); // activate partner
        }
    }

    // Notes previous and current observations
    void noteChanges()
    {
        prevPopulation = currPopulation;
        currPopulation = networkJarheads.Count;
    }
}
