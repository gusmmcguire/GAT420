using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateAgent owner, string name) : base(owner,name)
    {
        
    }

    public override void OnEnter()
    {
        owner.timer.value = 1;
        owner.movement.Stop();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        
    }
}
