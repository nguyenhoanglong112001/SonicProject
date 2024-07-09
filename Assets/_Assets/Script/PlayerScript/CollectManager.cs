using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] private float energydash;
    [SerializeField] private int redstartring;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        energydash = 0;
        redstartring = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRing()
    {
        return coins;
    }

    public void SetRing(int rings) => coins += rings;

    public float GetEnergyDash()
    {
        return energydash;
    }

    public void SetEnergyDash(float energy) => energydash += energy;

    public int GetRedRings()
    {
        return redstartring;
    }

    public void SetRedStartRing(int redrings) => redstartring += redrings;
}
