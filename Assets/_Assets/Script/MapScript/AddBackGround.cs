using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBackGround : MonoBehaviour
{
    [SerializeField] private CheckLane checkEnd;
    private void Start()
    {
        if(!checkEnd.isEndZone)
        {
            Zone currentZone = ZoneManager.instance.currentZone;
            Instantiate(ZoneManager.instance.bgDict[currentZone], transform.position, ZoneManager.instance.bgDict[currentZone].transform.rotation,this.transform);
        }
    }
}
