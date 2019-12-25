using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 target;
    public float speed = 20;
    public float offsetX = -5;

    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Only move when right arrow is pressed and when player is infront of field
        if(Input.GetAxisRaw("Horizontal") == 1 && player.transform.position.x + offsetX > transform.position.x)
        {
            float step = speed * Time.deltaTime;
            target = new Vector3(player.transform.position.x + offsetX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        
    }
}
