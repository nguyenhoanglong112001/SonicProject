using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRingCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CollectManager check;
    [SerializeField] private int redringsup;
    void Start()
    {
        check = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            check.SetRedStartRing(redringsup);
        }
    }
}