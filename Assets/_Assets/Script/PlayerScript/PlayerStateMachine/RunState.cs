using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {

    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collision collision)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.MoveForward();
    }
}
