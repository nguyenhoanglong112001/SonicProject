using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.isDash = true;
        Physics.IgnoreLayerCollision(player.playerlayer, player.blockerlayer, true);
        player.playeranimator.SetBool("Dash", true);
        player.SpeedUp(1.5f);
        player.StartCoroutine(player.DashTime());
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //player.MoveForward();
    }


    public override void ExitState(PlayerStateManager player)
    {

    }
}
