using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public float speed = 50;
    private float step;
    
    public List<GameObject> partyJarbuds;
    private List<Vector2> partyTargets;

    private Transform partyArea;
    private Collider partyAreaCollider;
    Vector2 size;
    Vector2 center;
    float top;
    float bottom;
    float left;
    float right;

    private GameObject hitObject;
    Vector2 point2d;
    Vector3 point3d;

    private Vector2 prevCollPos;
    private Vector2 currCollPos;
    private Vector2 targetPos;

    public int currPopulation;
    private int prevPopulation;

    public GameObject job;
    public GameObject housing;

    // Start is called before the first frame update
    void Start()
    {
        partyJarbuds = new List<GameObject>();
        partyTargets = new List<Vector2>();

        partyArea = transform.Find("PartyArea");
        partyAreaCollider = partyArea.GetComponent<Collider>();
        currPopulation = partyJarbuds.Count;
        currCollPos = partyArea.position;
    }

    // Fixed Update for physics
    void FixedUpdate()
    {
        noteChanges();
        if (currPopulation > prevPopulation)
        {
            decisionPoof();
            addTarget();
        }
        
        if (currCollPos != prevCollPos) updateTargets();
        if (currPopulation > 0) moveJarbuds();
    }

    // Notes previous and current observations
    void noteChanges()
    {
        prevPopulation = currPopulation;
        currPopulation = partyJarbuds.Count;

        prevCollPos = currCollPos;
        currCollPos = partyArea.position;
    }

    // Poofs other related contrasting decision jarbuds if there are any
    void decisionPoof()
    {
        if(partyJarbuds[partyJarbuds.Count-1].name.StartsWith("Job") && !job){
            job = partyJarbuds[partyJarbuds.Count-1];

            GameObject[] allJarbuds = GameObject.FindGameObjectsWithTag("Jarbud");
            foreach(GameObject jarbud in allJarbuds)
            {
                if(jarbud.name.StartsWith("Job") && jarbud != job)
                {
                    jarbud.SetActive(false);
                }
            }
        }

        if (partyJarbuds[partyJarbuds.Count-1].name.StartsWith("Housing") && !housing){
            housing = partyJarbuds[partyJarbuds.Count-1];

            GameObject[] allJarbuds = GameObject.FindGameObjectsWithTag("Jarbud");
            foreach (GameObject jarbud in allJarbuds)
            {
                if (jarbud.name.StartsWith("Housing") && jarbud != housing)
                {
                    jarbud.SetActive(false);
                }
            }
        }
    }
    
    // Adds a target to the partyAreaCollider
    void addTarget()
    {
        Debug.Log("Added to party!");

        int i = 0;
        do
        {
            calculateAreaPoints();
            float x = Random.Range(left + 2, right - 2);
            float y = Random.Range(bottom + 1, top - 1);
            point2d = new Vector2(x, y);
            i++;
            if (i == 100)
            {
                Debug.Log("As crowded as Malta...");
                break;
            }

        } while (isOccupied());

        partyTargets.Add(point2d);
    }

    // Checks if there's a something at that point
    bool isOccupied()
    {
        point3d = new Vector3(point2d.x, point2d.y, -10);
        RaycastHit hit;
        if (Physics.Raycast(point3d, transform.forward, out hit))
        {
            hitObject = hit.collider.gameObject;
            if (hitObject.tag == "JarbudFeet")
            {
                return true;
            }
        }
        return false;
        
    }

    // Update targets with area movement
    void updateTargets()
    {
        for(int i = 0; i < partyTargets.Count; i++)
        {
            targetPos = partyTargets[i];
            partyTargets[i] = new Vector2(targetPos.x + (currCollPos.x - prevCollPos.x),
                                            targetPos.y + (currCollPos.y - prevCollPos.y));
        }
    }

    // Calculates the bound locations of the party area collider
    void calculateAreaPoints()
    {
        size = partyAreaCollider.bounds.extents;
        center = partyAreaCollider.bounds.center;

        top = center.y + size.y;
        bottom = center.y - size.y;
        left = center.x - size.x;
        right = center.x + size.x;
    }

    // Moves jarbuds to targets
    void moveJarbuds()
    {
        for (int i = 0; i < currPopulation; i++)
        {
            Vector2 prevPos;
            Vector2 currPos;
            Vector2 currTarget;
            Vector2 velocity;

            step = speed * Time.deltaTime;
            prevPos = partyJarbuds[i].GetComponent<Transform>().position;
            currTarget = partyTargets[i];
           
            partyJarbuds[i].GetComponent<Transform>().position = Vector2.MoveTowards(prevPos, currTarget, step);
            currPos = partyJarbuds[i].GetComponent<Transform>().position;
            partyJarbuds[i].GetComponent<Transform>().Translate(0, 0, currPos.y + 18); // Maps depth

            // Note: Had to change from FixedUpdate to Update for the animator
            velocity = (currPos - prevPos) / Time.deltaTime;

            if(velocity.x < 0)
            {
                partyJarbuds[i].GetComponent<Transform>().Find("Jar").gameObject.GetComponent<SpriteRenderer>().flipX = true;
                partyJarbuds[i].GetComponent<Transform>().Find("Symbol").gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                partyJarbuds[i].GetComponent<Transform>().Find("Jar").gameObject.GetComponent<SpriteRenderer>().flipX = false;
                partyJarbuds[i].GetComponent<Transform>().Find("Symbol").gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            partyJarbuds[i].GetComponent<Transform>().Find("Jar").gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(velocity.x));
        }
    }
}
