using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private CollectManager check;
    // Start is called before the first frame update
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
            check.SetShield(true);
            Destroy(gameObject);
        }
    }
}
