using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        if(player.crouchCoroutine != null)
        {
            player.StopCoroutine(player.crouchCoroutine);
            player.crouchCoroutine = null;
        }
        player.playeranimator.SetTrigger("Roll");
        player.playerrigi.AddForce(Vector3.up * player.jumpforce,ForceMode.Impulse);
        player.isjump = true;
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
        if (player.isjump != true)
        {
            player.currentState = player.state.Run();
            player.SwitchState(player.currentState);
        }
    }
}
