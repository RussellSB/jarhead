using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalController : MonoBehaviour
{
    private GameObject player;
    Vector3 initPos;
    float prevX;
    float displacementX;

    public float intervalLength = 100f;
    public static float countdown;
    public static int intervalCount = 0;

    public GameObject Canvas;
    public GameObject BossCrowd;
    public GameObject Partner;
    public GameObject Child;

    public static bool spawnBoss = false;
    public static bool spawnChild = false;
    public static bool spawnPartner = false;

    public static bool causeWorkplacePrompt = false;
    public static bool causePartnerPrompt = false;
    public static bool causeChildPrompt = false;
    public static bool causeOtherPrompt = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initPos = player.transform.position;
        prevX = player.transform.position.x;
        countdown = intervalLength;

        spawnAll();

        causeWorkplacePrompt = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x != prevX)
        {
            displacementX = player.transform.position.x - prevX;
            countdown -= displacementX;
            prevX = player.transform.position.x;

            if (countdown <= 0)
            {
                newInterval();
            }

            if (causeWorkplacePrompt && (countdown <=  1 * intervalLength/4))
            {
                Canvas.GetComponent<DecisionPrompt>().Popup();
                causeWorkplacePrompt = false;
            }

            Debug.Log(displacementX + ", " + countdown + ", " + intervalCount);
        }
    }

    void newInterval()
    {
        countdown = intervalLength;
        Canvas.GetComponent<MonthlyPrompt>().Popup();

        // Refer to Ok() in monthly prompt for more changes

        causeWorkplacePrompt = true;
    }

    public void spawnAll()
    {
        BossCrowd.transform.position = new Vector2(player.transform.position.x + intervalLength, 0);
    }
}
