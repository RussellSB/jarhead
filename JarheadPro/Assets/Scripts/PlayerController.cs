using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float speed = 9;                         // Speed of player
    public float acceleration = 75;                 // Acceleration when starting to walk
    public float deceleration = 70;                 // Deceleration when slowing down
    private Vector3 velocity;                       // To store x and y velocity at the current update

    private Animator animator;
    private SpriteRenderer srenderer;
    
    // Decided on bounds
    private float maxY = -18f;
    private float maxZ = 1f;
    private float minY = -21f;
    private float minZ = -1.9f;

    public bool canGoUp = true;
    public bool canGoDown = true;

    float moveHor, moveVer;

    private void Awake()
    {
        animator = transform.Find("Body").GetComponent<Animator>();
        srenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVer = Input.GetAxisRaw("Vertical");

        // Horzontal Movement
        if (moveHor != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveHor, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        if(moveHor == -1)
        {
            srenderer.flipX = true;
        }
        else
        {
            srenderer.flipX = false;
        }

        // Vertical Movement
        checkBoundaries();
        if (moveVer != 0)
        {
            if (moveVer == 1 && canGoUp) accVer();
            if (moveVer == -1 && canGoDown) accVer();
        }
        else decVer();

        animator.SetFloat("Velocity", Mathf.Abs(velocity.x)+ Mathf.Abs(velocity.y));
        transform.Translate(velocity * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ForceField"))
        {
            srenderer.flipX = false;
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                animator.SetBool("isCollided", false);
            }
            else
            {
                animator.SetBool("isCollided", true);
            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ForceField"))
        {
            animator.SetBool("isCollided", false);
        }
    }

    private void accVer()
    {
        velocity.y = Mathf.MoveTowards(velocity.y, speed * moveVer, acceleration * Time.deltaTime);
        velocity.z = Mathf.MoveTowards(velocity.z, speed * moveVer, acceleration * Time.deltaTime);
    }

    private void decVer()
    {
        velocity.y = Mathf.MoveTowards(velocity.y, 0, deceleration * Time.deltaTime);
        velocity.z = Mathf.MoveTowards(velocity.z, 0, deceleration * Time.deltaTime);
    }

    private void checkBoundaries()
    {
        if (transform.Find("Collider").position.y >= minY && transform.Find("Collider").position.z >= minZ)
        {
            canGoDown = true;
        }
        else
        {
            canGoDown = false;
            decVer();
        }
        // Check if can go up (upper bound check)
        if (transform.Find("Collider").position.y <= maxY && transform.Find("Collider").position.z <= maxZ)
        {
            canGoUp = true;
        }
        else
        {
            canGoUp = false;
            decVer();
        }
    }
}