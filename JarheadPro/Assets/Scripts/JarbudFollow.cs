using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarbudFollow : StateMachineBehaviour
{
    public float speed = 11;
    public float acceleration = 75;                 // Acceleration when starting to walk
    public float deceleration = 70;                 // Deceleration when slowing down
    private Vector3 velocity;                       // To store x and y velocity at the current update

    public float x_offset = 3.5f; //3.5
    public float y_offset = 2.5f; //2.5

    private Vector2 movePosition;
    private Rigidbody rb2D;
    private Rigidbody rb2D_p;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Follow");
        rb2D = animator.GetComponent<Rigidbody>();
        rb2D_p = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetAxisRaw("Horizontal") != -1)
        {            
            float step = speed * Time.deltaTime;
            Vector3 target = new Vector3(rb2D_p.position.x - x_offset, rb2D_p.position.y - y_offset, rb2D_p.position.z);
            movePosition = Vector2.MoveTowards(rb2D.position, target, step);
            rb2D.MovePosition(movePosition);

            // If at target.... stop following and patrol
            if (rb2D.position == target)
            {
                animator.SetBool("isPatrolling", true);
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
