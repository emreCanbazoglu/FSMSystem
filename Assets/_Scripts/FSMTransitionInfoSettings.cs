using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class FSMTransitionInfo
{
    public FSMTransition TransitionEnum;
    public string TriggerName;
}

public class FSMTransitionInfoSettings : MonoBehaviour
{
    static FSMTransitionInfoSettings _instance;

    public static FSMTransitionInfoSettings Instance{ get { return _instance; } }

    public List<FSMTransitionInfo> TransitionInfoList;

    void Awake()
    {
        _instance = this;
    }

    void OnDestroy()
    {
        _instance = null;
    }

    public string GetTriggerName(FSMTransition transition)
    {
        try
        {
            return TransitionInfoList.Single(val => val.TransitionEnum == transition).TriggerName;
        }
        catch
        {
            return "";
        }
    }
}
