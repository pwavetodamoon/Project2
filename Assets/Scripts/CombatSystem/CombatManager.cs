using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public List<PlayerCharacters> characters = new List<PlayerCharacters>();
    public List<EnemyCharacters> enemies = new List<EnemyCharacters>();
    public static Func<EnemyCharacters> GetEnemyPosition;
    private void Awake()
    {
        GetEnemyPosition += GetEnemy;
    }
    private void OnDisable()
    {
        GetEnemyPosition -= GetEnemy;
    }
    EnemyCharacters GetEnemy()
    {
        if(enemies.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (var enemy in enemies)
            {
                if(enemy != null)
                {
                    return enemy;
                }
            }
            return null;
        }
    }
    public void Add(CharactersBase characters)
    {
        if(characters is PlayerCharacters)
        {
            this.characters.Add(characters as PlayerCharacters);
        }
        else if(characters is EnemyCharacters)
        {
            enemies.Add(characters as EnemyCharacters);
        }
    }
}
