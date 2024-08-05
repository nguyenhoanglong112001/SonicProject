using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] private float energydash;
    [SerializeField] private int redstartring;
    [SerializeField] private bool ismaget;
    [SerializeField] private GameObject magetlimit;
    [SerializeField] private float timeCD;
    [SerializeField] private bool isshield;
    [SerializeField] private bool isenerbeam;
    [SerializeField] private bool isSpring;

    public bool Isenerbeam { get => isenerbeam; set => isenerbeam = value; }
    public float Energydash { get => energydash; set => energydash = value; }
    public bool IsSpring { get => isSpring; set => isSpring = value; }

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
        Maget();
        Shield();
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

    public void SetRedStartRing(int redrings) => redstartring += redrings;

    private void Maget()
    {
        if(ismaget)
        {
            magetlimit.SetActive(true);
            StartCoroutine(MagetCountDown());
        }
        else
        {
            magetlimit.SetActive(false);
        }
    }

    private void Shield()
    {
        if(isshield)
        {
            StartCoroutine(ShieldCountDown());
        }
    }
    IEnumerator MagetCountDown()
    {
        yield return new WaitForSeconds(timeCD);
        ismaget = false;
    }

    IEnumerator ShieldCountDown()
    {
        yield return new WaitForSeconds(timeCD);
        isshield = false;
    }

    public void SetMaget(bool check)
    {
        ismaget = check;
    }

    public void SetShield(bool check)
    {
        isshield = check;
    }

    public bool CheckShield()
    {
        return isshield;
    }
}
