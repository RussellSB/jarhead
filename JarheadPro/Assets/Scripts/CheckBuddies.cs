using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuddies : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            if (collision.gameObject.CompareTag("Jarbud") && collision.gameObject.GetComponent<Transform>().position.x > transform.position.x)
            {
                animator.SetBool("isPatrolling", true);
            }
            else
            {
                animator.SetBool("isPatrolling", false);
            }
        }
        
    }
}
