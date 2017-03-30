using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public FSMController TargetFSM;

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TargetFSM.SetTransition(FSMTransition.StartMove);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            TargetFSM.SetTransition(FSMTransition.StopMove);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            TargetFSM.SetTransition(FSMTransition.JumpLeft);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            TargetFSM.SetTransition(FSMTransition.JumpRight);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            TargetFSM.SetTransition(FSMTransition.Die);
    }
}
