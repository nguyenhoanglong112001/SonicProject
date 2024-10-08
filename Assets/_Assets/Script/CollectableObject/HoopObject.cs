using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopObject : MonoBehaviour
{
    [SerializeField] private Grind speed;
    [SerializeField] private float speedUp;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameObject.FindWithTag("Player").GetComponent<Grind>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            speed.Speed += speedUp;
        }
    }
}
