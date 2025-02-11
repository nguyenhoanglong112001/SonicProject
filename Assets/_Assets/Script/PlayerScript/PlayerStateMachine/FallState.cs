using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playeranimator.SetBool("IsFalling", true);
        player.playerrigi.velocity = Vector3.down * player.fallspeed * Time.deltaTime;
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.playeranimator.SetBool("IsFalling", false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //player.MoveForward();
        if(player.checkCondition.GroundCheck())
        {
            player.newState = player.state.Run();
            player.SwitchState(player.newState);
        }
    }
}
