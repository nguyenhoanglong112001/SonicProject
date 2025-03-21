using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
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
        change.SwitchToCharacter();
        playeranimator.SetTrigger(parameter);
        playerrigi.linearVelocity = Vector3.forward * knock * -1 * Time.deltaTime;
        PlayerManager.instance.isAlive = false;
        deathPos = transform.position;
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
        if (CollectManager.instance.GetRing() > 0)
        {
            Physics.IgnoreLayerCollision(playerlayer, enemylayer, true);
            Physics.IgnoreLayerCollision(playerlayer, blockerlayer, true);
            StartCoroutine(Ignore());
            playeranimator.SetTrigger("Stumble");
            CollectManager.instance.SetRing(-CollectManager.instance.GetRing());
        }
        else if (CollectManager.instance.GetRing() <= 0)
        {
            if (enemy.GetComponentInChildren<EnemyAttackIdentify>() != null)
            {
                setEnemyAttack = enemy.gameObject.GetComponentInChildren<EnemyAttackIdentify>();
                setEnemyAttack.AttackOn = false;
            }
            Death("Death1");
        }
    }

    private void KillEnemy(GameObject enemyHit)
    {
        if (ballcheck.isball || player.currentState is DashState || CollectManager.instance.CheckShield())
        {
            if (CollectManager.instance.CheckShield())
            {
                CollectManager.instance.SetShield(false);
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
        gameObject.transform.position = deathPos;
        DisableObject();
        StartCoroutine(DelayTimeSpawn());
    }

    IEnumerator DelayTimeSpawn()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(spawnTime);
        Time.timeScale = 1f;
        playeranimator.SetTrigger("Revive");
        player.currentState = player.state.Run();
        player.currentState.EnterState(player);
        PlayerManager.instance.isAlive = true;
    }

    private void DisableObject()
    {
        Collider[] hitobj = Physics.OverlapSphere(transform.position, rangerCheck, layerCheck);
        foreach (Collider col in hitobj)
        {
            Vector3 toObj = col.transform.position - transform.position;

            if(Vector3.Dot(transform.forward,toObj.normalized) > 0 )
            {
                Destroy(col.gameObject);
            }
        }
    }
}
