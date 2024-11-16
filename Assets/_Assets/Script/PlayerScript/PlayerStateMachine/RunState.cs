using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {

    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
    }

    public override void OnTriggerEnter(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //player.transform.Translate(Vector3.forward * player.speed * Time.deltaTime);
        //player.playerrigi.velocity = Vector3.forward * player.speed * Time.deltaTime;
        Vector3 forwardMovement = Vector3.forward * player.speed * Time.deltaTime;
        player.playerrigi.MovePosition(player.playerrigi.position + forwardMovement);
    }
}
