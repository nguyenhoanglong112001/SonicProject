using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagetCollect : MonoBehaviour
{
    [SerializeField] private CollectManager checkmaget;
    // Start is called before the first frame update
    void Start()
    {
        checkmaget = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            checkmaget.SetMaget(true);
            Destroy(gameObject);
        }    
    }
}
