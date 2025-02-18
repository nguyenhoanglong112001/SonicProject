using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerbeamState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.enerbeamRail = GameObject.FindWithTag("EnerbeamRail");
        player.currentPath = player.enerbeamRail.GetComponent<WayPointList>().wayPoints;
        player.playeranimator.SetTrigger("StartEnerbeam");
        player.startPos = player.transform.position;
        player.ener = player.SpawnEnerbeam();
        player.ener.GetComponent<Enerbeam>().path[0] = player.transform;
        player.ener.GetComponent<Enerbeam>().path[1] = player.currentPath[0];
        player.ener.GetComponent<Enerbeam>().path[2] = player.currentPath[1];
        player.playerrigi.isKinematic = true;
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.transform.SetParent(null);
        player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        player.DestroyEnerbeam();
        player.playerrigi.isKinematic = false;
        CollectManager.instance.Isenerbeam = false;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //if(Vector3.Distance(player.transform.position, player.enerbeamRail.transform.position) >= 0.1f)
        //{
        //    
        //    player.transform.position = Vector3.MoveTowards(player.startPos, player.enerbeamRail.transform.position, player.speedToRail * Time.deltaTime);
        // }
    }
}
