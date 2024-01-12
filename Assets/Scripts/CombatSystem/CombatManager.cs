using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] HeroCharacter[] Hero;
    [SerializeField]
    MonsterCharacter[] Monster;
    public static CombatManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public MonsterCharacter GetMonster()
    {
        if (Monster.Length == 0) return null;
        return Monster[0];
    }
    public int GetMonsterCount()
    {
        return Monster.Length;
    }
    [Button]
    void GetAllCharacter()
    {
        Hero = FindObjectsOfType<HeroCharacter>();
        foreach (HeroCharacter character in Hero)
        {
            Debug.Log(character.name);
        }
        Monster = FindObjectsOfType<MonsterCharacter>();
        foreach (MonsterCharacter character in Monster)
        {
            Debug.Log(character.name);
        }
    }
}