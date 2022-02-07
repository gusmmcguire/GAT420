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
        Debug.Log(name + " enter");
    }

    public override void OnExit()
    {
        Debug.Log(name + " exit");
    }

    public override void OnUpdate()
    {
        Debug.Log(name + " update");
        //timer -= Time.deltaTime;
        if (Time.time > timer)
        {
            owner.stateMachine.SetState(owner.stateMachine.StateFromName("patrol"));
        }
    }
}
