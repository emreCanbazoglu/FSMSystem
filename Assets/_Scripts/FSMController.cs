using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum FSMType
{
    None,
    Movement,
    Battle,
}

public enum FSMStateID
{
    NullState,
    Idle,
    Move,
    Stand,
    Jump,
    Dead,
}

public enum FSMTransition
{
    Null,
    StartMove,
    StopMove,
    JumpLeft,
    JumpRight,
    Die,
}

public class FSMController : MonoBehaviour
{
    public FSMType FSMType;
    
    public List<FSMStateID> FSMStateList;

    public Animator FSM;

    public FSMStateID CurStateID{ get; private set; }

    public FSMStateID PrevStateID{ get; private set; }

    FSMTransition _lastTransition;

    List<FSMStateBehaviour> _fsmStateBehaviourList;

    #region Events

    public event Action<FSMTransition, FSMStateID> OnStateChanged;

    void FireOnStateChanged()
    {
        Debug.Log("state changed with transition: <color=cyan>" + _lastTransition + "</color> new state: <color=magenta>" + CurStateID + "</color>");
        
        if (OnStateChanged != null)
            OnStateChanged(_lastTransition, CurStateID);
    }

    #endregion

    void Awake()
    {
        InitFSM();

        StartListeningEvents();
    }

    void OnDestroy()
    {
        FinishListeningEvents();
    }

    void InitFSM()
    {
        _fsmStateBehaviourList = FSM.GetBehaviours<FSMStateBehaviour>().ToList();
    }

    void StartListeningEvents()
    {
        _fsmStateBehaviourList.ForEach(val => val.OnStateEntered += OnStateEntered);
        _fsmStateBehaviourList.ForEach(val => val.OnStateExited += OnStateExited);
    }

    void FinishListeningEvents()
    {
        _fsmStateBehaviourList.ForEach(val => val.OnStateEntered -= OnStateEntered);
        _fsmStateBehaviourList.ForEach(val => val.OnStateExited -= OnStateExited);
    }

    public void SetParameter(string parameterName, object parameterValue)
    {
        TypeCode tc = Type.GetTypeCode(parameterValue.GetType());

        switch (tc)
        {
            case TypeCode.Int32:
                FSM.SetInteger(parameterName, (int)parameterValue);
                break;
            case TypeCode.Single:
                FSM.SetFloat(parameterName, (float)parameterValue);
                break;
            case TypeCode.Boolean:
                FSM.SetBool(parameterName, (bool)parameterValue);
                break;
        }
    }

    public void SetTransition(FSMTransition transition)
    {
        string transitionTrigger = FSMTransitionInfoSettings.Instance.GetTriggerName(transition);

        if (string.IsNullOrEmpty(transitionTrigger))
            return;

        _lastTransition = transition;

        FSM.SetTrigger(transitionTrigger);
    }

    void OnStateEntered(FSMStateID stateID)
    {
        PrevStateID = stateID;
        
        CurStateID = stateID;

        FireOnStateChanged();
    }

    void OnStateExited(FSMStateID stateID)
    {
        
    }
}
