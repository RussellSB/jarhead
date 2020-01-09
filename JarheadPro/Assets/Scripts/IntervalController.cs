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
    public static float distanceTillNextCheckpoint;
    public static int intervalCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initPos = player.transform.position;
        prevX = player.transform.position.x;

        distanceTillNextCheckpoint = intervalLength;
    }

    // Update is called once per frame
    void Update()
    {


        if(player.transform.position.x != prevX)
        {
            displacementX = player.transform.position.x - prevX;
            distanceTillNextCheckpoint -= displacementX;
            prevX = player.transform.position.x;

            if (distanceTillNextCheckpoint <= 0)
            {
                distanceTillNextCheckpoint = intervalLength;
                intervalCount++;
            }

            //Debug.Log(displacementX + ", " + distanceTillNextCheckpoint + ", " + intervalCount);
        }


       
    }
}
