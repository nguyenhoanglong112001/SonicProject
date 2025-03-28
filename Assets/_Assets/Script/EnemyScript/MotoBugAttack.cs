using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoBugAttack : MonoBehaviour
{
    [SerializeField] private EnemyAttackIdentify checkplayer;
    [SerializeField] private AudioSource motobugSource;
    [SerializeField] private Animator motorbugAttack;
    [SerializeField] private GameObject bugPos;
    [SerializeField] private float speed;
    [SerializeField] private Transform attackRange;
    private Vector3 playerpos;

    public Vector3 Playerpos { get => playerpos; set => playerpos = value; }

    public void Attack()
    {
        motorbugAttack.SetTrigger("Attack");
        Vector3 currentpos = bugPos.transform.position;
        //float newZ = Mathf.MoveTowards(currentpos.z, playerpos.z, speed * Time.deltaTime);
        //bugPos.transform.position = new Vector3(currentpos.x, currentpos.y, newZ);
        SoundManager.instance.PlaySound(motobugSource, SoundManager.instance.motorBugPass);
        Vector3 target = new Vector3(currentpos.x,currentpos.y,playerpos.z);
        float distance = Vector3.Distance(bugPos.transform.position, playerpos);
        float duration = distance / speed;
        Debug.Log(distance);
        bugPos.transform.DOMove(target, duration).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        //;
        //if(Mathf.Approximately(bugPos.transform.position.z,playerpos.z))
        //{

        //}
    }

    private void Update()
    {
    }

    private void Start()
    {
        attackRange.localPosition = Vector3.zero;
    }
}
