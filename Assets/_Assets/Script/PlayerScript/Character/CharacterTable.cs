using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public enum BonusType
{
    EnemyBonus,
    DistanceBonus,
    ComboBonus,
}
[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class CharacterTable : ScriptableObject
{
    public List<Character> CharacterList = new List<Character>();
}

[System.Serializable]
public class Character
{
    public int id;
    public string characterName;
    public BonusType bonusType;
    public Sprite CharacterImage;
    public GameObject CharacterPrefab;
    public Avatar CharacterAvatar;
    public AnimatorController CharacterAnimator;
    public AnimatorOverrideController CharacterOverrideAnimator;
}
