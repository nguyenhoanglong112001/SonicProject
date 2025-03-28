using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.isDash = true;
        if(player.voice.dashVoice != null )
        {
            SoundManager.instance.PlaySound(player.voice.source, player.voice.dashVoice);
        }
        foreach(Transform vfx in player.DashVFX.transform)
        {
            vfx.gameObject.SetActive(true);
            vfx.GetComponent<ParticleSystem>().Play();
        }
        SoundManager.instance.PlayTwoSound(player.dashSound, SoundManager.instance.startDashSound, SoundManager.instance.onDashSound,true);
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
        SoundManager.instance.StopSound(player.dashSound);
        player.dashEndVFX.gameObject.SetActive(false);
    }
}
