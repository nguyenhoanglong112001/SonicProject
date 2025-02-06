using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.isTurn = true;
        player.MoveWayPoint();
    }

    public override void UpdateState(PlayerStateManager player)
    {
    }

    public override void ExitState(PlayerStateManager player)
    {
    }
}
