using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLane : MonoBehaviour
{
    [SerializeField] private List<GameObject> listRoad;
    [SerializeField] private Dictionary<int, TypeRoad> roadDict;

    public List<GameObject> ListRoad { get => listRoad; set => listRoad = value; }
    public Dictionary<int, TypeRoad> RoadDict { get => roadDict; set => roadDict = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
