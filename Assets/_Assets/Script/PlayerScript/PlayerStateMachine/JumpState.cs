using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        if (player.crouchCoroutine != null)
        {
            player.StopCoroutine(player.crouchCoroutine);
            player.crouchCoroutine = null;
        }
        if(!player.isball)
        {
            player.playeranimator.SetTrigger("Roll");
            player.StartCoroutine(player.WaitToChangeBall());
        }
        player.playerrigi.AddForce(player.transform.up * player.jumpforce,ForceMode.Impulse);
        player.isjump = true;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //player.MoveForward();
        if(!player.isjump)
        {
            player.newState = player.state.Run();
            player.SwitchState(player.newState);
        }
    }
    public override void ExitState(PlayerStateManager player)
    {

    }
}
