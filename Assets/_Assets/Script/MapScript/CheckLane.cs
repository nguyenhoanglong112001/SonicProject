using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLane : MonoBehaviour
{
    [SerializeField] private Dictionary<float, TypeRoad> road;

    public Dictionary<float, TypeRoad> Road { get => road; set => road = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Road = new Dictionary<float, TypeRoad>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddValueToDict(float value, TypeRoad key)
    {
        Road.Add(value, key);
    }
}
