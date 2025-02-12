using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Zone
{
    Zone1 = 1,
    Zone2 = 2,
    Zone4 = 3,
}
public class ZoneManager : MonoBehaviour
{
    public static ZoneManager instance;
    public Zone currentZone;
    public GameObject bgZone1;
    public GameObject bgZone2;
    public GameObject bgZone4;
    public Dictionary<Zone, GameObject> bgDict;

    public GameObject endZone1;
    public GameObject endZone2;
    public GameObject endZone4;
    public Dictionary<Zone, GameObject> endZoneDict;

    public GameObject startZone1;
    public GameObject startZone2;
    public GameObject startZone4;

    public Dictionary<Zone, GameObject> startZonedict;

    public Transform startPos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        bgDict = new Dictionary<Zone, GameObject>()
        {
            {Zone.Zone1,bgZone1},
            {Zone.Zone2,bgZone2},
            {Zone.Zone4,bgZone4}
        };

        endZoneDict = new Dictionary<Zone, GameObject>()
        {
            {Zone.Zone1,endZone1},
            {Zone.Zone2,endZone2},
            {Zone.Zone4,endZone4}
        };

        startZonedict = new Dictionary<Zone, GameObject>()
        {
            {Zone.Zone1,startZone1},
            {Zone.Zone2,startZone2},
            {Zone.Zone4,startZone4}
        };
        SpawnStart();
    }

    private void SpawnStart()
    {
        Instantiate(startZonedict[currentZone], startPos.position, startZonedict[currentZone].transform.rotation, startPos);
    }
}
