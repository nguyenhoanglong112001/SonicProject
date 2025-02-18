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
        int totalGain = SaveManager.instance.GetIntData(SaveKey.GoldRingBank, 0);
        SaveManager.instance.Save(SaveKey.GoldRingBank, totalGain+CollectManager.instance.GetRing());
        CollectManager.instance.SetRing(-CollectManager.instance.GetRing());
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
