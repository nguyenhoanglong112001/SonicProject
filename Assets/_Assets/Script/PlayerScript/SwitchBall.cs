using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private Material ballmaterial;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private Mesh ballmesh;
    [SerializeField] private Mesh charactermesh;
    [SerializeField] private Avatar characterAvatar;
    [SerializeField] private Avatar ballAvatar;
    [SerializeField] private RuntimeAnimatorController ballanimator;
    [SerializeField] private RuntimeAnimatorController characteranimator;
    [SerializeField] private CapsuleCollider charactercollider;
    [SerializeField] private SphereCollider ballcollider;
    [SerializeField] private InputManager checkcondition;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager checkcollect;

    public bool isball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkcondition.Isjumping)
        {
            if(checkcondition.GroundCheck())
            {
                SwitchToCharacter();
                checkcondition.Isjumping = false;
            }
        }    
    }

    private void Switch(Mesh meshchange, Material material, RuntimeAnimatorController animator, Avatar avatar, bool active1, bool active2)
    {
        mesh.sharedMesh = meshchange;
        mesh.material = material;
        playeranimator.runtimeAnimatorController = animator;
        playeranimator.avatar = avatar;
        ballcollider.enabled = active1;
        charactercollider.enabled = active2;
    }

    public void ChangeBall()
    {
        Debug.Log("Ball");
        Switch(ballmesh, ballmaterial, ballanimator, ballAvatar, true, false);
    }

    public void SwitchToCharacter()
    {
        Debug.Log("sonic");
        Switch(charactermesh, characterMaterial, characteranimator, characterAvatar, false, true);
    }
}
