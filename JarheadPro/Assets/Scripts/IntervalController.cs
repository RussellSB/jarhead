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

    public static bool causeWorkplacePrompt;
    public static bool causePartnerPrompt;
    public static bool causeChildPrompt;
    public static bool causeOtherPrompt;

    public static List<DecisionScenario> otherLibrary;
    public static List<DecisionScenario> workplaceLibrary;
    public static List<DecisionScenario> partnerLibrary;
    public static List<DecisionScenario> childLibrary;

    public static float income;
    public static float expense;

    // Start is called before the first frame update
    void Awake()
    {
        causeWorkplacePrompt = false;
        causePartnerPrompt = false;
        causeChildPrompt = false;
        causeOtherPrompt = true;

        player = GameObject.FindGameObjectWithTag("Player");
        initPos = player.transform.position;
        prevX = player.transform.position.x;
        countdown = intervalLength;

        income = 0;
        expense = 0;

        initDecisionLibraries();
        spawnAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x != prevX)
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
        if (BossCrowd.transform.position.y == 0) causeWorkplacePrompt = true;

        //The prompt for jarheads with decision to add to network
        if (NetworkController.decided_partner) causePartnerPrompt = true;
        else Partner.transform.position = new Vector2(Partner.transform.position.x, 50f);

        if (NetworkController.decided_child) causeChildPrompt = true;
        else Child.transform.position = new Vector2(Child.transform.position.x, 50f);

        // The special new jarheads
        if (intervalCount == 2) Partner.transform.position = new Vector2(Partner.transform.position.x, -15f);
        if (intervalCount == 3 && NetworkController.decided_partner) Child.transform.position = new Vector2(Child.transform.position.x, -17.5f);

        Debug.Log("Expense: " + expense);
        Debug.Log("Income: " + income);
    }

    public void spawnAll()
    {
        // Jarhead sprites
        BossCrowd.transform.position = new Vector2(player.transform.position.x + intervalLength, BossCrowd.transform.position.y);
        Child.transform.position = new Vector2(player.transform.position.x + (intervalLength / 2) + (intervalLength / 20), Child.transform.position.y);
        Partner.transform.position = new Vector2(player.transform.position.x + (intervalLength / 2) - (intervalLength / 20), Partner.transform.position.y);

        // Other sprites
        Other.transform.position = new Vector2(player.transform.position.x + intervalLength / 4, -11.5f);
        House.transform.position = new Vector2(player.transform.position.x + (intervalLength / 2), House.transform.position.y);
        Workplace.transform.position = new Vector2(player.transform.position.x + 3 * intervalLength / 4, Workplace.transform.position.y);
    }

    public void activateBoss()
    {
        BossCrowd.transform.position = new Vector2(BossCrowd.transform.position.x, 0);
        Workplace.transform.position = new Vector2(Workplace.transform.position.x, 0);
        causeWorkplacePrompt = true; // also activates workplace prompt
    }

    public void activateHouse()
    {
        House.transform.position = new Vector2(House.transform.position.x, -2.9f);
    }

    public void activatePartner()
    {
        Partner.transform.position = new Vector2(Partner.transform.position.x, -15f);
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Partner");
        //causePartnerPrompt = true; // Will be true on next interval of activation
    }

    public void activateChild()
    {
        Child.transform.position = new Vector2(Child.transform.position.x, -17.5f);
        GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>().PlaySound("Child");
        //causeChildPrompt = true; // Will be true on next interval of activation
    }

    public static void SummonJarbud(string jarname)
    {
        GameObject jarbud = GameObject.Find(jarname);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        jarbud.transform.position = new Vector3(player.transform.position.x + 60, -18, 0);
        jarbud.GetComponent<Scrolling>().enabled = false;
        PartyController.partyJarbuds.Add(jarbud);
    }

    void otherDecision()
    {
        int i = Random.Range(0, otherLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Other", otherLibrary[i]);
        if (!otherLibrary[i].isRepeatable) otherLibrary.RemoveAt(i);
    }

    void partnerDecision()
    {
        int i = Random.Range(0, partnerLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Partner", partnerLibrary[i]);
        if (!partnerLibrary[i].isRepeatable) partnerLibrary.RemoveAt(i);
    }

    void childDecision()
    {
        int i = Random.Range(0, childLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Child", childLibrary[i]);
        if (!childLibrary[i].isRepeatable) childLibrary.RemoveAt(i);
    }

    void workplaceDecision()
    {
        int i = Random.Range(0, workplaceLibrary.Count);
        Canvas.GetComponent<DecisionPrompt>().Popup("Work Place", workplaceLibrary[i]);
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
        otherLibrary.Add(new DecisionScenario(
            "Your car broke down and needs to be fixed. If you don't fix it, you'll have more time to overthink with all the distances you must then walk.",
            true,
            "FixBrokenCar",
            "ProExistential",
            option1: "Fix it",
            option2: "Let it be"));
        otherLibrary.Add(new DecisionScenario(
            "You're already tired of your old furniture. What do you think? Buy new furniture?",
            true,
            "BuyNewFurniture",
            "ProExistential",
            option1: "Go buy",
            option2: "Go sty"));
        otherLibrary.Add(new DecisionScenario(
            "Your dishwasher just broke down again! What are the odds, really. Will you buy a new one?",
            true,
            "BuyNewDishwasher",
            "ProExistential",
            option1: "Oof, fine",
            option2: "No, sink-life"));
        otherLibrary.Add(new DecisionScenario(
            "Your friend just had a bunch of puppies and offered you one. You love puppies. Will you take it?",
            false,
            "TakePuppy",
            "ProExistential",
            option1: "Barkin' time",
            option2: "Goodbye, dog"));
        otherLibrary.Add(new DecisionScenario(
            "You read an article stating that hobbies are a good way to maintain your overall mental well being. Will you try get a hobby?",
            false,
            "InvestPasstime",
            "ProExistential"));
        otherLibrary.Add(new DecisionScenario(
            "You go to the supermarket. There's an offer to get Pringles 80% off, but you have to buy a year's worth of pringles. Are you in?",
            false,
            "Summon-EffectPringles&BuyPringles",
            "AvoidPringles",
            option1: "Sure",
            option2: "No. (Wrong choice)"));
    }

    void initPartnerLibrary()
    {
        partnerLibrary = new List<DecisionScenario>();
        partnerLibrary.Add(new DecisionScenario(
            "Your partner keeps on hinting that you two should go out. It's a special occasion! ... again. Will you go out for dinner?",
            true,
            "ProLover&NetflixAndChill",
            "AntiLover"));
        partnerLibrary.Add(new DecisionScenario(
            "It's Valentine's day... A lot of other jarheads seem to be getting their partners gifts as grand gestures. Buy a gift?",
            false,
             "ProLover&ValentinesDay",
             "AntiLover"));
        partnerLibrary.Add(new DecisionScenario(
            "Your anniversary is coming up. Your partner takes this quite seriously. What will you do?",
            false,
            "ProLover&JarheadChild",
            "AntiLover",
            option1: "Plan",
            option2: "Chill"));
        partnerLibrary.Add(new DecisionScenario(
            "Your partner has been wanting to go on a particular trip outside Europe for a while now. Shall you book it, to surprise them? ",
            false,
            "ProLover&PartnerTripPlan",
            "AntiLover"));
        partnerLibrary.Add(new DecisionScenario(
            "Wow! Your partner found a new TV show that seems actually worth your time. Start watching a new TV show together? ",
            true,
            "ProLover",
            "AntiLover"));
        partnerLibrary.Add(new DecisionScenario(
            "Your partner is in the mood for games night. Play a board game together? ",
            true,
            "ProLover",
            "AntiLover"));
        partnerLibrary.Add(new DecisionScenario(
            "Your aniversary is coming, better start planning for it. (Plan or not)",
            true,
            "ProLover&anniversaryPlan",
            "AntiLover"));
    }

    void initChildLibrary()
    {
        childLibrary = new List<DecisionScenario>();
        childLibrary.Add(new DecisionScenario(
            "Private schools are known to be better than public schools, albeit more expensive. What will you send your child to?",
            false,
            "ProParenting&ChildPrivate",
            "AntiParenting",
            option1: "Public",
            option2: "Private"));
        childLibrary.Add(new DecisionScenario(
            "Your child wants you to buy them a new video game. Will you buy it?",
            true,
            "ProParenting&ChildBuyNewToys",
            "AntiParenting"));
        childLibrary.Add(new DecisionScenario(
            "The workload for homework keeps on growing. Your child is clearly struggling. Will you help?",
            true,
            "ProParenting&HelpChildHomework",
            "AntiParenting"));
        childLibrary.Add(new DecisionScenario(
            "It's your child's birthday, and he's been eyeing that new toy for a while. Buy a gift??",
            false,
            "ProParenting&ChildToyBirthday",
            "AntiParenting"));
        childLibrary.Add(new DecisionScenario(
            "Your child's sick and needs medicine. Will you leave it up to nature or visit the pharmacy?",
            false,
            "ProParenting&ChildSickMedicine",
            "AntiParenting",
            option1: "Pharmacy",
            option2: "Nature"));
    }

    void initWorkplaceLibrary()
    {
        workplaceLibrary = new List<DecisionScenario>();
        workplaceLibrary.Add(new DecisionScenario(
            "A senior employee decided to retire early from his post, and the boss chose you to take his place. Do you accept the promotion?",
            false,
            "ProWork&WorkPromotionAverage",
            "CHOICE2ID_PLACEHOLDER"));
        workplaceLibrary.Add(new DecisionScenario(
            "Your team is behind on a project and has decided to work overtime to manage. Will you stay and work?",
            true,
            "ProWork&TeamWorkOvertime",
            "CHOICE2ID_PLACEHOLDER"));
        workplaceLibrary.Add(new DecisionScenario(
           "The boss decided to have an office party. Going? (hope he doesn't get cold feet)",
           true,
           "BossOfficeParty",
           "CHOICE2ID_PLACEHOLDER"));
        workplaceLibrary.Add(new DecisionScenario(
            "Your boss was not happy with your previous report and wants you to redo it by its original deadline. Work overtime?",
            true,
            "ProWork&RedoReportQuickly",
            "CHOICE2ID_PLACEHOLDER"));
        workplaceLibrary.Add(new DecisionScenario(
            "An employee messed up. They're in trouble and needs someone to cover for them. Will you help?",
            true,
            "ProWork&EmployeeCover",
            "CHOICE2ID_PLACEHOLDER"));
        workplaceLibrary.Add(new DecisionScenario(
            "The office decided to plan a going away party for a fellow employee. Will you go?",
            true,
            "ProWork&GoingAwayParty",
            "CHOICE2ID_PLACEHOLDER"));
        workplaceLibrary.Add(new DecisionScenario(
            "An employee is having a hard time in his personal life, so everyone decided to give some money in order to help. Chip in?",
            true,
            "ProWork&HelpEmployeePersonal",
            "CHOICE2ID_PLACEHOLDER"));
    }
}
