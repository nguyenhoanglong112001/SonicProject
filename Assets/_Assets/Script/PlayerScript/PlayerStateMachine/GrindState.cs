using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("EnterState");
        SoundManager.instance.PlaySound(player.playerSound, SoundManager.instance.grindSound, true);
        player.playerrigi.isKinematic = true;
        player.playeranimator.SetTrigger("StartGrind");
        player.playeranimator.SetBool("Grind", true);
        player.switchcheck.SwitchToCharacter();
        player.EnterSpline(player.OnCompletRail);
    }


    public override void UpdateState(PlayerStateManager player)
    {
        //player.InputChangeRail();
    }

    public override void ExitState(PlayerStateManager player)
    {
        SoundManager.instance.StopSound(player.playerSound);
        player.playeranimator.SetBool("Grind", false);
        player.gameObject.transform.position += player.newpos;
        player.israil = false;
    }
}
