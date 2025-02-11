using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.isTurn = true;
        player.playerrigi.isKinematic = true;
        player.Turn();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Vector3 newEuler = player.transform.eulerAngles;
        newEuler.z = 0;
        player.transform.eulerAngles = newEuler;
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.isTurn = false;
        player.lane = 1;
    }
}
