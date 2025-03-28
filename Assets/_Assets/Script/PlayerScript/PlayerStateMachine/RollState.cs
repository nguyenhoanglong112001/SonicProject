using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        if(player.isjump == true)
        {
            Debug.Log("land");
            SoundManager.instance.PlaySound(player.playerSound, SoundManager.instance.landSound);
            player.isjump = false;
            player.playerrigi.linearVelocity = Vector3.down * player.landSpeed * Time.deltaTime;
        }
        if(!player.isball)
        {
            player.playeranimator.SetTrigger("Roll");
        }
        player.Crouch();
        player.TurnOnSlanVFX();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //player.MoveForward();
    }

    public override void ExitState(PlayerStateManager player)
    {

    }
}
