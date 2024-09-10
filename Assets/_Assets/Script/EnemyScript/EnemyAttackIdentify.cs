using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackIdentify : MonoBehaviour
{
    private bool attackOn;
    [SerializeField] private MotoBugAttack bugAttack;

    public bool AttackOn { get => attackOn; set => attackOn = value; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AttackOn = true;
            bugAttack.Playerpos = other.transform.position;
            gameObject.SetActive(false);
        }
    }
}
