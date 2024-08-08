using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeRoad
{
    Road,
    Hillup,
    HillDown,
    GapRoad,
    Rail,
}
public class RoadType : MonoBehaviour
{
    public TypeRoad type;
}
