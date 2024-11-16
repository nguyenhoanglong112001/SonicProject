using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playeranimator.SetTrigger("Roll");
        player.playerrigi.AddForce(Vector3.up * player.jumpforce,ForceMode.Impulse);
        player.isjump = true;
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
    }

    public override void OnTriggerEnter(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.currentState = player.state.Run();
        player.SwitchState(player.currentState);
    }
}
