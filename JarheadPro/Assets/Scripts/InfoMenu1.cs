using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu1 : MonoBehaviour
{
    public bool isDecided;
    public GameObject decide;
    public GameObject decided;

    // Start is called before the first frame update
    void Start()
    {
        isDecided = false;
    }

    public void Decide()
    {
        isDecided = true;
        decide.SetActive(false);
        decided.SetActive(true);
        
        GameObject parent = gameObject.transform.parent.gameObject;
        GameObject grandparent = parent.transform.parent.gameObject; // grandparent is jarhead
        
        NetworkController.networkJarheads.Add(grandparent);

        GameObject infoBubble = grandparent.transform.Find("infoBubble1").gameObject;
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Click");
        Animator animator = infoBubble.GetComponent<Animator>();
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);

    }
}
