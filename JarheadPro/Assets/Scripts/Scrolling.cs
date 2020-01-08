using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public bool scrolling, parallaxEntity;

    public float backgroundSize;
    public float parallaxSpeed;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;
    private float lastPlayerX;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerX = player.transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {

        if (parallaxEntity && Input.GetAxisRaw("Horizontal") == 1)
        {
            float deltaX = player.transform.position.x - lastPlayerX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
        }
        lastPlayerX = player.transform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
        
    }

    void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize,
                                                    transform.position.y, transform.position.z);              //Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundSize,
                                                    transform.position.y, transform.position.z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
