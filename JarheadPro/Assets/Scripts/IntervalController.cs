using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalController : MonoBehaviour
{
    private GameObject player;
    Vector3 initPos;
    float prevX;
    float displacementX;

    public float intervalLength;
    public static float countdown;
    public static int intervalCount = 1;

    public GameObject Canvas;

    public GameObject BossCrowd;
    public GameObject Workplace;
    public GameObject Partner;
    public GameObject Child;
    public GameObject House;
    public GameObject Other;

    public static bool causeWorkplacePrompt = false;
    public static bool causePartnerPrompt = false;
    public static bool causeChildPrompt = false;
    public static bool causeOtherPrompt = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initPos = player.transform.position;
        prevX = player.transform.position.x;
        countdown = intervalLength;

        spawnAll();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x != prevX)
        {
            displacementX = player.transform.position.x - prevX;
            countdown -= displacementX;
            prevX = player.transform.position.x;

            // At other
            if (causeOtherPrompt && player.transform.position.x >= Other.transform.position.x)
            {
                Canvas.GetComponent<DecisionPrompt>().Popup();
                causeOtherPrompt = false;
            }

            // At partner
            if (causePartnerPrompt && player.transform.position.x >= Partner.transform.position.x)
            {
                Canvas.GetComponent<DecisionPrompt>().Popup();
                causePartnerPrompt = false;
            }

            // At child
            if (causeChildPrompt && player.transform.position.x >= Child.transform.position.x)
            {
                Canvas.GetComponent<DecisionPrompt>().Popup();
                causeChildPrompt = false;
            }

            // At workplace
            if (causeWorkplacePrompt && player.transform.position.x >= Workplace.transform.position.x)
            {
                Canvas.GetComponent<DecisionPrompt>().Popup();
                causeWorkplacePrompt = false;
            }

            // At the end (boss)
            if (player.transform.position.x >= BossCrowd.transform.position.x)
            {
                Canvas.GetComponent<MonthlyPrompt>().Popup();
            }

            Debug.Log(displacementX + ", " + countdown + ", " + intervalCount);
        }
    }

    public void newInterval()
    {
        countdown = intervalLength;
        intervalCount++;
        spawnAll();

        if (intervalCount == 3) activatePartner();
        if (intervalCount == 6 && Partner.activeInHierarchy) activateChild();

        if (BossCrowd.activeInHierarchy) causeWorkplacePrompt = true;
        if (Partner.activeInHierarchy) causePartnerPrompt = true;
        if (Child.activeInHierarchy) causeChildPrompt = true;
        causeOtherPrompt = true; // Always active
    }

    public void spawnAll()
    {
        // With sprites
        BossCrowd.transform.position = new Vector2(player.transform.position.x + intervalLength, 0);
        Child.transform.position = new Vector2(player.transform.position.x + (intervalLength / 2) + (intervalLength / 20), -17.5f);
        Partner.transform.position = new Vector2(player.transform.position.x + (intervalLength / 2) - (intervalLength / 20), -15f);
        House.transform.position = new Vector2(player.transform.position.x + (intervalLength / 2), -2.9f);

        // Not jarhead
        Other.transform.position = new Vector2(player.transform.position.x + intervalLength/4, -11.5f);
        Workplace.transform.position = new Vector2(player.transform.position.x + 3 * intervalLength / 4, 0);
    }

    public void activateBoss()
    {
        BossCrowd.SetActive(true);
        Workplace.SetActive(true);
        causeWorkplacePrompt = true; // also activates workplace prompt
    }

    public void activateHouse()
    {
        House.SetActive(true);
    }

    public void activatePartner()
    {
        Partner.SetActive(true);
        causePartnerPrompt = true;
    }

    public void activateChild()
    {
        Child.SetActive(true);
        causeChildPrompt = true;
    }
}
