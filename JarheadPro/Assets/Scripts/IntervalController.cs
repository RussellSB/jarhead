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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initPos = player.transform.position;
        prevX = player.transform.position.x;
        countdown = intervalLength;
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

            Debug.Log(displacementX + ", " + countdown + ", " + intervalCount);
        }
    }

    void newInterval()
    {
        countdown = intervalLength;
        Canvas.GetComponent<MonthlyPrompt>().Popup();
        intervalCount++;

        if (spawnBoss) spawnBossAtEnd();
    }

    public void spawnBossAtEnd()
    {
        BossCrowd.transform.position = new Vector2(player.transform.position.x + intervalLength, 0);
    }
}
