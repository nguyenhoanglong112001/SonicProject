using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLane : MonoBehaviour
{
    [SerializeField] private Dictionary<float, GameObject> road;
    public bool isEndZone;
    public Dictionary<float, GameObject> Road { get => road; set => road = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Road = new Dictionary<float, GameObject>();
        RandomEndZone();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddValueToDict(float value, GameObject key)
    {
        Road.Add(value, key);
    }

    private void RandomEndZone()
    {
        int a = Random.Range(0, 100);
        isEndZone = (a > 90) ? true : false;
    }
}
