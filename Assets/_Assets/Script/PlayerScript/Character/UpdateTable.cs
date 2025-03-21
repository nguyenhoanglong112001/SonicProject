using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum CostCurrency
{
    Gold,
    RedRing
}
[CreateAssetMenu(fileName = "UpdateTable", menuName = "Scriptable Objects/UpdateTable")]
public class UpdateTable : ScriptableObject
{
    public List<UpdateInfo> updateList;
}

[System.Serializable]
public class UpdateInfo
{
    public int level;
    public int bonus;
    public CostCurrency costType;
    public int cost;
}
