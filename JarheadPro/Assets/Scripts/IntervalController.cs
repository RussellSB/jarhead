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

    public static List<DecisionScenario> otherLibrary;
    public static List<DecisionScenario>  workplaceLibrary;
    public static List<DecisionScenario> partnerLibrary;
    public static List<DecisionScenario> childLibrary;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initPos = player.transform.position;
        prevX = player.transform.position.x;
        countdown = intervalLength;

        initDecisionLibraries();
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
                otherDecision();
                causeOtherPrompt = false;
            }

            // At partner
            if (causePartnerPrompt && player.transform.position.x >= Partner.transform.position.x)
            {
                partnerDecision();
                causePartnerPrompt = false;
            }

            // At child
            if (causeChildPrompt && player.transform.position.x >= Child.transform.position.x)
            {
                childDecision();
                causeChildPrompt = false;
            }

            // At workplace
            if (causeWorkplacePrompt && player.transform.position.x >= Workplace.transform.position.x)
            {
                workplaceDecision();
                causeWorkplacePrompt = false;
            }

            // At the end (boss)
            if (player.transform.position.x >= BossCrowd.transform.position.x)
            {
                Canvas.GetComponent<MonthlyPrompt>().Popup();
            }

            //Debug.Log(displacementX + ", " + countdown + ", " + intervalCount);
        }
    }

    public void newInterval()
    {
        countdown = intervalLength;
        intervalCount++;
        spawnAll();

        // The prompts
        causeOtherPrompt = true; // Always active
        if (BossCrowd.activeInHierarchy) causeWorkplacePrompt = true;

        //The prompt for jarheads with decision to add to network
        if (NetworkController.decided_partner) causePartnerPrompt = true;
        else Partner.SetActive(false);

        if (NetworkController.decided_child) causeChildPrompt = true;
        else Child.SetActive(false);

        // The special new jarheads
        if (intervalCount == 2) Partner.SetActive(true);
        if (intervalCount == 3 && NetworkController.decided_partner) Child.SetActive(true);
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
        //causePartnerPrompt = true; // Will be true on next interval of activation
    }

    public void activateChild()
    {
        Child.SetActive(true);
        //causeChildPrompt = true; // Will be true on next interval of activation
    }

    void otherDecision()
    {
        int i = Random.Range(0, otherLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Other", otherLibrary[i].getDecision(), otherLibrary[i].getOption1(), otherLibrary[i].getOption2());
        if (!otherLibrary[i].isRepeatable) otherLibrary.RemoveAt(i);
    }

    void partnerDecision()
    {
        int i = Random.Range(0, partnerLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Partner", partnerLibrary[i].getDecision(), partnerLibrary[i].getOption1(), partnerLibrary[i].getOption2());
        if (!partnerLibrary[i].isRepeatable) partnerLibrary.RemoveAt(i);
    }

    void childDecision()
    {
        int i = Random.Range(0, childLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Child", childLibrary[i].getDecision(), childLibrary[i].getOption1(), childLibrary[i].getOption2());
        if (!childLibrary[i].isRepeatable) childLibrary.RemoveAt(i);
    }

    void workplaceDecision()
    {
        int i = Random.Range(0, workplaceLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Work Place", workplaceLibrary[i].getDecision(), workplaceLibrary[i].getOption1(), workplaceLibrary[i].getOption2());
        if (!workplaceLibrary[i].isRepeatable) workplaceLibrary.RemoveAt(i);
    }

    void initDecisionLibraries()
    {
        initOtherLibrary();
        initChildLibrary();
        initPartnerLibrary();
        initWorkplaceLibrary();
    }

    void initOtherLibrary()
    {
        otherLibrary = new List<DecisionScenario>();
        otherLibrary.Add(new DecisionScenario("You read an article stating that passtimes are good way to maintain your overall mental well being. Will you start getting invested in one?", false));
        otherLibrary.Add(new DecisionScenario("Your car broke down and needs to be fixed", "Fix it", "Let it be", true));
        otherLibrary.Add(new DecisionScenario("Your house could really use some new furniture.", "Go buy", "Go sty", false));
        otherLibrary.Add(new DecisionScenario("Your dishwasher just broke down. Will you buy a new one?", "Oof, fine", "No, sinktime", false));
        otherLibrary.Add(new DecisionScenario("Your friend just had a bunch of puppies and offered you one. Will you take it?", false));
        otherLibrary.Add(new DecisionScenario("You read an article stating that passtimes are good way to maintain your overall mental well being. Will you start getting invested in one?", false));
  }

    void initPartnerLibrary()
    {
        partnerLibrary = new List<DecisionScenario>();
        partnerLibrary.Add(new DecisionScenario("Your partner keeps hinting that you two should go out. Will you go out for dinner?", true));
        partnerLibrary.Add(new DecisionScenario("It's Valentine's day...come on. Buy a gift?", false));
        partnerLibrary.Add(new DecisionScenario("Your aniversary is coming up, you better start planning for it.", "Plan", "Nah", false));
        partnerLibrary.Add(new DecisionScenario("You two have been planning this trip for a while now. Have you decided on going? ", false));
        partnerLibrary.Add(new DecisionScenario("Wow! Your partner found a new TV show that seems actually worth your time. Start a new TV Show together? ", true));
        partnerLibrary.Add(new DecisionScenario("Your partner is in the mood for games night. Play a board game together? ", true));
        //partnerLibrary.Add(new DecisionScenario("Your partner has been talking an awful lot about children recently. We know what that means...hopefully. Have a child?", false, true));
    }

    void initChildLibrary()
    {
        childLibrary = new List<DecisionScenario>();
        childLibrary.Add(new DecisionScenario("Private schools are known to be better than public schools, albeit more expensive. What will you send your child to?", "Public", "Private", false));
        childLibrary.Add(new DecisionScenario("Your child wants to buy a new video game. Will you buy it?", true));
        childLibrary.Add(new DecisionScenario("The workload for homework keeps on growing. Your child is clearly struggling. Will you help?", true));
        childLibrary.Add(new DecisionScenario("It's your child's birthday, and he's been eyeing that new toy for a while. Buy a gift??", false));
        childLibrary.Add(new DecisionScenario("Your child's sick and needs medicine. Will you leave it up to nature or visit the pharmacy?", "Nature", "Pharmacy", true));
    }

    void initWorkplaceLibrary()
    {
       workplaceLibrary = new List<DecisionScenario>();
       workplaceLibrary.Add(new DecisionScenario("A senior employee decided to retire early from his post, and the boss chose you to take his place. Do you accept the promotion?", false));
       workplaceLibrary.Add(new DecisionScenario("Your team is behind on a project and has decided to work overtime to manage. Will you stay and work?", true));
       workplaceLibrary.Add(new DecisionScenario("The boss decided to have a work party. Let's hope he doesn't get cold feet. Are you Going?", true));
       workplaceLibrary.Add(new DecisionScenario("Your boss was not happy with your previous report and wants you to redo it by its original deadline. Work overtime?", false));
       workplaceLibrary.Add(new DecisionScenario("Your co-worker needs somebody to cover them while they deal with a situation with their family. Will you help?", false));
       workplaceLibrary.Add(new DecisionScenario("The office decided to plan a going away party for a fellow employee. Will you go?", true));
       workplaceLibrary.Add(new DecisionScenario("Your co-worker is having a hard time with medical problems, so everyone decided to give some money in order to help. Chip in?", false));
    }
}
