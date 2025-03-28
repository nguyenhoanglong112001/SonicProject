using DG.Tweening;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private PlayerStateManager player;
    [SerializeField] private Rigidbody playerrigi;
    [SerializeField] private float knock;
    [SerializeField] private SwitchBall ballcheck;
    [SerializeField] private SwitchBall change;
    [SerializeField] private int playerlayer;
    [SerializeField] private int enemylayer;
    [SerializeField] private int blockerlayer;
    [SerializeField] private EnemyAttackIdentify setEnemyAttack;
    [SerializeField] private int enemyScore;
    [SerializeField] private MutiplyerScript mutiply;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask raillayer;
    [SerializeField] private GameObject magetLimit;
    [SerializeField] private CharacterVoice voice;
    public Transform[] wayPoints;
    public bool isTurn;
    //public bool isalive;
    public bool _canDodge;

    [Header("======For revive")]
    public Vector3 deathPos;
    public float spawnTime;
    public float rangerCheck;
    public LayerMask layerCheck;

    public Animator playeranimator;
    // Start is called before the first frame update
    void Start()
    {
        CollectManager.instance.magetlimit = magetLimit;
        voice = GetComponentInChildren<CharacterVoice>();
        SoundManager.instance.PlaySound(voice.source, voice.introVoice);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool GroundCheck()
    {
        bool isGround = Physics.Raycast(transform.position, Vector3.down, 1.0f, groundlayer);

        if (!isGround)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, raillayer);
        }

        return isGround;
    }

    private void Death(string parameter)
    {
        SoundManager.instance.PlaySound(player.playerSound, SoundManager.instance.deathSound);
        SoundManager.instance.StopSound(SoundManager.instance.bgSound);
        change.SwitchToCharacter();
        playeranimator.SetTrigger(parameter);
        transform.DOJump(transform.position + Vector3.forward * knock * -1, 0.5f, 1, 0.5f);
        playerrigi.isKinematic = true;
        PlayerManager.instance.isAlive = false;
        SaveManager.instance.Save(SaveKey.RedRing, CollectManager.instance.GetRedRingCollect());
        GameManager.instance.ChangeGameState(GameState.EndGame);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Blocker"))
        {
            if(player.currentState is not DashState && !CollectManager.instance.CheckShield())
            {
                if (ballcheck.isball)
                {
                    ballcheck.SwitchToCharacter();
                    Death("Death1");
                }
                Death("Death1");
            }
            else if (player.currentState is not DashState || CollectManager.instance.CheckShield())
            {
                Destroy(collision.gameObject);
                CollectManager.instance.SetShield(false);
            }
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            KillEnemy(collision.gameObject);
            if(_canDodge)
            {
                _canDodge = false;
            }
        }
        if(collision.gameObject.CompareTag("Laser"))
        {
            HitEnemy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("NonGround"))
        {
            if (player.currentState is not DashState)
            {
                ballcheck.SwitchToCharacter();
                playeranimator.SetTrigger("DeathFall");
                PlayerManager.instance.isAlive = false;
                GameManager.instance.ChangeGameState(GameState.EndGame);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DoubleMutiply"))
        {
            StartCoroutine(MutiplyerCountDown());
        }    
        if(other.CompareTag("Hoop"))
        {
            ComboUpdate("Hoop");
        }
        if(other.CompareTag("Dodge"))
        {
            _canDodge = true;
        }
        if(other.CompareTag("StartTurn") && isTurn == false)
        {
            isTurn = true;
            player.follower.spline = other.gameObject.GetComponent<SplineComputer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dodge"))
        {
            if(_canDodge)
            {
                _canDodge = false;
            }
        }
    }

    private void HitEnemy(GameObject enemy)
    {
        SoundManager.instance.PlaySound(voice.source, voice.hurtVoice);
        if (CollectManager.instance.GetRing() > 0)
        {
            Physics.IgnoreLayerCollision(playerlayer, enemylayer, true);
            Physics.IgnoreLayerCollision(playerlayer, blockerlayer, true);
            StartCoroutine(Ignore());
            playeranimator.SetTrigger("Stumble");
            SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.ringdropSound);
            CollectManager.instance.SetRing(-CollectManager.instance.GetRing());
        }
        else if (CollectManager.instance.GetRing() <= 0)
        {
            if (enemy.GetComponentInChildren<EnemyAttackIdentify>() != null)
            {
                setEnemyAttack = enemy.gameObject.GetComponentInChildren<EnemyAttackIdentify>();
            }
            Death("Death1");
        }
    }

    private void KillEnemy(GameObject enemyHit)
    {
        if (ballcheck.isball || player.currentState is DashState || CollectManager.instance.CheckShield())
        {
            EnemyType type = enemyHit.GetComponentInChildren<TypeEnemy>().type;
            CheckEnemyHitSound(type);
            if (CollectManager.instance.CheckShield())
            {
                CollectManager.instance.SetShield(false);
                player.ShieldVFX.SetActive(false);
                player.ShieldendVFX.SetActive(true);
                player.ShieldendVFX.GetComponent<ParticleSystem>().Play();
                UIIngameManager.instance.ShowCombotype("Enemy");
            }
            if(player.currentState is DashState)
            {
                ComboManager.instance.UpdateCombo();
                UIIngameManager.instance.ShowCombotype("Smash");
            }
            else
            {
                if(CharacterManager.instance.bonusType == BonusType.EnemyScore)
                {
                    ScoreManager.instance.UpdateScore(enemyScore * (CharacterManager.instance.bonus/100));
                }
                else
                {
                    ScoreManager.instance.UpdateScore(enemyScore);
                }
                ComboManager.instance.UpdateCombo();
                UIIngameManager.instance.ShowCombotype("Enemy");
            }
            Destroy(enemyHit);
        }
        else
        {
            HitEnemy(enemyHit);
        }
    }

    private void CheckEnemyHitSound(EnemyType typeEnemy)
    {
        switch(typeEnemy)
        {
            case EnemyType.Crab:
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.hitEmenySource, SoundManager.instance.hitCrabSound);
                    break;
                }
            case EnemyType.Bee:
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.hitEmenySource, SoundManager.instance.hitBeeSound);
                    break;
                }
            case EnemyType.MotoBug:
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.hitEmenySource, SoundManager.instance.hitMotoBugSound);
                    break;
                }
        }
    }

    public void ComboUpdate(string comboType)
    {
        ComboManager.instance.UpdateCombo();
        UIIngameManager.instance.ShowCombotype(comboType);
    }

    IEnumerator Ignore()
    {
        yield return new WaitForSeconds(2.0f);
        Physics.IgnoreLayerCollision(playerlayer, enemylayer, false);
        Physics.IgnoreLayerCollision(playerlayer, blockerlayer, false);
    }

    IEnumerator MutiplyerCountDown()
    {
        mutiply.Mutiplyer *= 2;
        yield return new WaitForSeconds(5.0f);
        mutiply.Mutiplyer /= 2;
    }  
    

    public void PlayerRevive()
    {
        gameObject.transform.position = new Vector3(0,gameObject.transform.position.y,gameObject.transform.position.z);
        DisableObject();
        StartCoroutine(DelayTimeSpawn());
    }

    IEnumerator DelayTimeSpawn()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(spawnTime);
        SoundManager.instance.PlayCurrentSound(SoundManager.instance.bgSound);
        playerrigi.isKinematic = false;
        player.lane = 1;
        Time.timeScale = 1f;
        playeranimator.SetTrigger("Revive");
        player.currentState = player.state.Run();
        player.currentState.EnterState(player);
        PlayerManager.instance.isAlive = true;
    }

    private void DisableObject()
    {
        RaycastHit[] hitobj = Physics.RaycastAll(transform.position,transform.forward,rangerCheck,layerCheck);
        foreach (var col in hitobj)
        {
            Vector3 toObj = col.transform.position - transform.position;

            if(Vector3.Dot(transform.forward,toObj.normalized) > 0 )
            {
                if(col.collider.gameObject.layer == LayerMask.NameToLayer("Blocker"))
                {
                    Destroy(col.collider.gameObject.transform.parent.gameObject);
                }    
                else if (col.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Destroy(col.collider.gameObject);
                }
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,transform.forward * rangerCheck);
    }
#endif
}
