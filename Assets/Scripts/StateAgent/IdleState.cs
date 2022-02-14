using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    float timer;
    public IdleState(StateAgent owner, string name) : base(owner,name)
    {
        
    }

    public override void OnEnter()
    {
        owner.timer.value = 2;
        owner.movement.Stop();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (Time.time > timer)
        {
            owner.stateMachine.SetState(owner.stateMachine.StateFromName("patrol"));
        }
    }
}
