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
            if(ballcheck.isball || checkdash.isdashing || checkcollect.CheckShield())
            {
                Destroy(collision.gameObject);
                if(checkcollect.CheckShield())
                {
                    checkcollect.SetShield(false);
                }
            }
            else
            {
                if (checkcollect.GetRing() > 0 && !checkcollect.CheckShield())
                {
                    checkcollect.SetRing(-checkcollect.GetRing());
                    Physics.IgnoreLayerCollision(playerlayer, enemylayer);
                    Physics.IgnoreLayerCollision(playerlayer, blockerlayer);
                    playeranimator.SetTrigger("Stumble");
                }
                else if (checkcollect.GetRing() < 0 && !checkcollect.CheckShield())
                {
                    Death("Death1");
                }
            }
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
}
