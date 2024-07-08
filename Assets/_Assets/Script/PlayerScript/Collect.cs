using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField]private int coin;
    // Start is called before the first frame update
    void Start()
    {
        coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetCoin()
    {
        return coin;
    }

    public void SetCoin(int set)
    {
        coin += set;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ring"))
        {
            coin += 1;
            Destroy(other.gameObject);
        }
    }
}
