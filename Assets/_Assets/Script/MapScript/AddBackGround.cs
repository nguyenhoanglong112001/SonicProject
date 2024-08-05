using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBackGround : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;
    [SerializeField] private Transform pos;
    private GameObject back;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(back == null)
            {
                back = Instantiate(BackGround, pos.position, BackGround.transform.rotation);
            }
        }
    }
}
