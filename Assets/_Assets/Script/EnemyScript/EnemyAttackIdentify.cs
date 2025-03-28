using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackIdentify : MonoBehaviour
{
    [SerializeField] private MotoBugAttack bugAttack;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bugAttack.Playerpos = other.transform.position;
            bugAttack.Attack();
            gameObject.SetActive(false);
        }
    }
}
