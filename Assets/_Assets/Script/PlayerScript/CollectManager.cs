using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private bool isDouble;
    [SerializeField] private bool isOrbMaget;
    [SerializeField] private Image fillbar;
    [SerializeField] private Text textcoin;
    [SerializeField] private Text textRedRing;

    public bool Isenerbeam { get => isenerbeam; set => isenerbeam = value; }
    public float Energydash { get => energydash; set => energydash = value; }
    public bool IsSpring { get => isSpring; set => isSpring = value; }
    public bool Ismaget { get => ismaget; set => ismaget = value; }
    public bool IsDouble { get => isDouble; set => isDouble = value; }
    public bool IsOrbMaget { get => isOrbMaget; set => isOrbMaget = value; }

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
        Maget(Ismaget);
        Maget(IsOrbMaget);
        Shield();
        if(Energydash <= 100)
        {
            SetEnergyDash(Energydash);
        }
        SetTextRing(coins);
        SetRedStartRing(redstartring);
    }

    private void SetEnergyDash(float energy)
    {
        fillbar.fillAmount = energy / 100;
    }

    public void SetTextRing(int textring)
    {
        textcoin.text = coins.ToString();
    }

    public void SetTextRedRing(int redring)
    {
        textRedRing.text = redstartring.ToString();
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
    private void Maget(bool maget)
    {
        if(maget)
        {
            magetlimit.SetActive(true);
            StartCoroutine(PowerCountDow(maget));
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
            StartCoroutine(PowerCountDow(isshield));
        }
    }
    IEnumerator PowerCountDow(bool checkPara)
    {
        yield return new WaitForSeconds(timeCD);
        checkPara = false;
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
