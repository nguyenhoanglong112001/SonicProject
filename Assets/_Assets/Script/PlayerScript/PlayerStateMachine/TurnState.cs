using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TurnState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.isTurn = true;
        player.playerrigi.isKinematic = true;
        SaveManager.instance.Save("Rings", player.check.GetRing());
        player.check.SetRing(-player.check.GetRing());
        player.Turn();
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }

    public override void ExitState(PlayerStateManager player)
    {
        player.lane = 1;
        player.playerrigi.isKinematic = false;
    }
}
