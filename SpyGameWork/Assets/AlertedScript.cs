using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AlertedScript : StateMachineBehaviour
{

    private AICharacterControl script;
    private float alertedExitTimer;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        script = animator.transform.root.GetComponent<AICharacterControl>();
        alertedExitTimer = 5f;
        if (animator.GetBool("raisedSuspicion"))
            animator.SetBool("raisedSuspicion", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (script.Seen())
        {
            if (GameManager.instance.isAggressive)
            {
                animator.SetBool("isInCombat", true);
            }
            else if (GameManager.instance.isNaughty)
            {
                animator.SetBool("inDialogue", true);
            }
        }
        else if (alertedExitTimer >= 0)
        {
            alertedExitTimer -= Time.deltaTime;
            Debug.Log("Alert Exit timer:" + alertedExitTimer);
        }
        if (alertedExitTimer <= 0)
        {
            animator.SetBool("isAlerted", false);
            animator.SetBool("raisedSuspicion", true);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
