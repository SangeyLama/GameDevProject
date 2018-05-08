using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class RaisedSuspicionScript : StateMachineBehaviour {

    private AICharacterControl script;
    private float suspicionExitTimer;
    private float detectionTimer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        script = animator.transform.root.GetComponent<AICharacterControl>();
        detectionTimer = 3f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {        
        if (script.Seen())
        {
            suspicionExitTimer = 5f;
            detectionTimer -= Time.deltaTime;
            Debug.Log("Detection in: " + detectionTimer);
        }
            
        else if(suspicionExitTimer >= 0)
        {
            detectionTimer = 3f;
            suspicionExitTimer -= Time.deltaTime;
            //Debug.Log("Timer: " + suspicionExitTimer);
        }

        if (detectionTimer <= 0)
        {
            animator.SetBool("isAlerted", true);
        }            

        if (suspicionExitTimer <= 0)
            animator.SetBool("raisedSuspicion", false);
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
