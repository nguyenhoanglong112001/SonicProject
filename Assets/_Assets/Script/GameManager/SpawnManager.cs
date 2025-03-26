using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<GameObject> blockesJump;
    public List<GameObject> blockRolls;
    public List<GameObject> blockTripleJump;

    public GameObject GetBlocker(BlockerType type)
    {
        List<GameObject> blockZone = new List<GameObject>();
        switch(type)
        {
            case BlockerType.Jump:
                {
                    AddBlockToList(blockesJump, blockZone);
                    int a = Random.Range(0, blockZone.Count);
                    return blockZone[a];
                }
            case BlockerType.Roll:
                {
                    AddBlockToList(blockRolls, blockZone);
                    int a = Random.Range(0, blockZone.Count);
                    return blockZone[a];
                }
            case BlockerType.TripleJump:
                {
                    AddBlockToList(blockTripleJump, blockZone);
                    int a = Random.Range(0, blockZone.Count);
                    return blockZone[a];
                }
        }
        return null;
    }

    private void AddBlockToList(List<GameObject> objes,List<GameObject> blockZone)
    {
        foreach (GameObject obj in objes)
        {
            if (obj.GetComponent<BlockType>().zone == ZoneManager.instance.currentZone)
            {
                blockZone.Add(obj);
            }
        }
    }
}
