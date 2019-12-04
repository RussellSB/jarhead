using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarbudPatrol : StateMachineBehaviour
{
    // Update is called once per frame
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            animator.SetBool("isPatrolling", false);
        }
    }
}
