using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int currentGold;
    public int currentRedRing;
    public CostConfig cost;

    public UnityEvent<int> OnUpdateGold;
    public UnityEvent<int> OnUpdateRedRing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        currentGold = SaveManager.instance.GetIntData(SaveKey.CurrentGold, 0);
        currentRedRing = SaveManager.instance.GetIntData(SaveKey.CurrentRedRing,0);
    }

    public void UpdateGoldRing(int goldUpdate)
    {
        currentGold += goldUpdate;
        SaveManager.instance.Save(SaveKey.CurrentGold,currentGold);
        OnUpdateGold?.Invoke(currentGold);
    }

    public void UpdateRedRing(int redring)
    {
        currentRedRing += redring;
        SaveManager.instance.Save(SaveKey.RedRing,currentRedRing);
        OnUpdateRedRing?.Invoke(currentRedRing);
    }
}
