using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("EnterState");
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
        player.gameObject.transform.DOKill();
        player.playeranimator.SetBool("Grind", false);
        player.gameObject.transform.position += player.newpos;
        player.israil = false;
    }
}
