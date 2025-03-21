using System.Collections.Generic;
using UnityEngine;

public static class SaveKey
{
    public static string RedRing = "RedRing";
    public static string GoldRingBank = "GoldRingGain";
    public static string CurrentGold = "GoldRing";
    public static string CurrentRedRing = "CurrentRedRing";
    public static string LeadRunner = "LeadRunner";
    public static string SideRunner1 = "SideRunner1";
    public static string SideRunner2 = "SideRunner2";

    public static Dictionary<int, string> characterLv = new Dictionary<int, string>()
    {
        {1,"SonicLv" },
        {2,"TailsLv" },
        {3,"Knuckleslv" },
        {4,"AmyLv" },
        {5,"Stickslv" },
        {6,"ShadowLv" },
        {7,"VectorLv" }
    };

    public static string ListCharacterBuy = "CharacterUnlock";
}
