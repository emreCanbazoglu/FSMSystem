using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMStateBehaviour : StateMachineBehaviour
{
    public FSMStateID StateID;

    #region Events

    public Action<FSMStateID> OnStateEntered;

    void FireOnStateEntered()
    {
        if (OnStateEntered != null)
            OnStateEntered(StateID);
    }

    public Action<FSMStateID> OnStateExited;

    void FireOnStateExited()
    {
        if (OnStateExited != null)
            OnStateExited(StateID);
    }

    #endregion

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        FireOnStateEntered();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        FireOnStateExited();
    }
}
