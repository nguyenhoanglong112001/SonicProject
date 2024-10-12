using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private Rigidbody playerrigi;
    [SerializeField] private float knock;
    [SerializeField] private SwitchBall ballcheck;
    [SerializeField] private DashPower checkdash;
    [SerializeField] private CollectManager checkcollect;
    [SerializeField] private SwitchBall change;
    [SerializeField] private int playerlayer;
    [SerializeField] private int enemylayer;
    [SerializeField] private int blockerlayer;
    [SerializeField] private EnemyAttackIdentify setEnemyAttack;
    [SerializeField] private ScoreManager updateScore;
    [SerializeField] private ComboManager comboUpdate;
    [SerializeField] private ComboUI typeChange;
    [SerializeField] private int enemyScore;
    [SerializeField] private MutiplyerScript mutiply;
    [SerializeField] private float duration;
    [SerializeField] private float fadeTime;
    [SerializeField] private Material characterMat;

    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask raillayer;
    public bool isalive;
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
        playerrigi.velocity = Vector3.forward * knock * -1 * Time.deltaTime;
        isalive = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Blocker"))
        {
            if(!checkdash.isdashing && !checkcollect.CheckShield())
            {
                if (ballcheck.isball)
                {
                    ballcheck.SwitchToCharacter();
                    Death("Death1");
                }
                Death("Death1");
            }
            else if (!checkdash.isdashing || checkcollect.CheckShield())
            {
                Destroy(collision.gameObject);
                checkcollect.SetShield(false);
            }
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            KillEnemy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("NonGround"))
        {
            if (!checkdash.isdashing)
            {
                ballcheck.SwitchToCharacter();
                playeranimator.SetTrigger("DeathFall");
                isalive = false;
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
            CollectHoop(other.gameObject);
        }
    }

    private void HitEnemy(GameObject enemy)
    {
        if (checkcollect.GetRing() > 0)
        {
            Physics.IgnoreLayerCollision(playerlayer, enemylayer, true);
            Physics.IgnoreLayerCollision(playerlayer, blockerlayer, true);
            StartCoroutine(Ignore());
            playeranimator.SetTrigger("Stumble");
            checkcollect.SetRing(-checkcollect.GetRing());
        }
        else if (checkcollect.GetRing() <= 0)
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
        if (ballcheck.isball || checkdash.isdashing || checkcollect.CheckShield())
        {
            if (checkcollect.CheckShield())
            {
                checkcollect.SetShield(false);
                typeChange.ShowCombotype("Enemy");
            }
            if(checkdash.isdashing)
            {
                comboUpdate.UpdateCombo();
                typeChange.ShowCombotype("Smash");
            }
            else
            {
                updateScore.UpdateScore(enemyScore);
                comboUpdate.UpdateCombo();
                typeChange.ShowCombotype("Enemy");
            }
            Destroy(enemyHit);
        }
        else
        {
            HitEnemy(enemyHit);
        }
    }

    private void CollectHoop(GameObject hoop)
    {
        comboUpdate.UpdateCombo();
        typeChange.ShowCombotype("Hoop");
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
    
    IEnumerator CharacterFade()
    {
        for (int i = 0; i <fadeTime;i++)
        {
            SetAlpha(0f);
            yield return new WaitForSeconds(duration);

            SetAlpha(1f);
            yield return new WaitForSeconds(duration);
        }
    } 
    
    private void SetAlpha(float alpha)
    {
        Color color = characterMat.color;
        color.a = alpha;
        characterMat.color = color;
    }    
}
