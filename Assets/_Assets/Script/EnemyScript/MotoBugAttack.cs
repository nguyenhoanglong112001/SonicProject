using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoBugAttack : MonoBehaviour
{
    [SerializeField] private EnemyAttackIdentify checkplayer;
    [SerializeField] private Animator motorbugAttack;
    [SerializeField] private GameObject bugPos;
    [SerializeField] private float speed;
    private Vector3 playerpos;

    public Vector3 Playerpos { get => playerpos; set => playerpos = value; }

    private void Attack()
    {
        if(checkplayer.AttackOn)
        {
            motorbugAttack.SetTrigger("Attack");
            Vector3 currentpos = bugPos.transform.position;
            float newZ = Mathf.MoveTowards(currentpos.z, playerpos.z, speed * Time.deltaTime);
            bugPos.transform.position = new Vector3(currentpos.x, currentpos.y, newZ);
        }
    }

    private void Update()
    {
        Attack();
    }
}
