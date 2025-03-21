using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    [Header("====== Character Shop=======")]
    public CharacterTable CharacterInfo;
    public List<int> listCharacterBuy = new List<int>();
    public Button BuyBt;
    public List<GameObject> listObjectCost = new List<GameObject>();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        listCharacterBuy = SaveManager.instance.LoadListInt(SaveKey.ListCharacterBuy);
        if(listCharacterBuy.Count <= 0 )
        {
            listCharacterBuy.Add(1);
        }    
        SetShopStart();
    }

    public void OnBuySucced(int id)
    {
        listCharacterBuy.Add(id);
        SaveManager.instance.SaveListint(listCharacterBuy,SaveKey.ListCharacterBuy);
    }

    private void SetShopStart()
    {
        foreach(Character character in CharacterInfo.CharacterList)
        {
            if(listCharacterBuy.Contains(character.id))
            {
                character.IsUnlock = true;
                listObjectCost[character.id -1].SetActive(false);
            }
            else
            {
                character.IsUnlock = false;
                character.currentlevel = 1;
                listObjectCost[character.id - 1].SetActive(true);
            }
        }
    }
}
