using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public List<PlayerCharacters> characters = new List<PlayerCharacters>();
    public List<EnemyCharacters> enemies = new List<EnemyCharacters>();
    public static Func<EnemyCharacters> GetEnemyPosition;
    public static Action<CharactersBase> RemoveAction;
    static List<Action> actionsListPlayer = new List<Action>();
    static List<Action> actionsListMonster = new List<Action>();



    public static void AddPlayerAction(Action action)
    {
        actionsListPlayer.Add(action);
    }
    public static void AddMonsterAction(Action action)
    {
        actionsListMonster.Add(action);
    }
    private void Start()
    {
        StartCoroutine(CheckingReapting());
    }
    IEnumerator CheckingReapting()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            CheckAttack(actionsListPlayer, enemies.Count);
            //CheckAttack(actionsListPlayer, characters.Count);
        }
    }

    private void CheckAttack(List<Action> actionsList, int count)
    {
        if (actionsList.Count > 0 && count > 0)
        {
            foreach (var action in actionsList)
            {
                Debug.Log("Attack Callback");
                action?.Invoke();
                //actionsList.Remove(action);
            }
            actionsList.Clear();
        }
    }
    private void Awake()
    {
        GetEnemyPosition += GetEnemy;
        RemoveAction += Remove;
    }
    private void OnDisable()
    {
        GetEnemyPosition -= GetEnemy;
        RemoveAction -= Remove;
    }
    EnemyCharacters GetEnemy()
    {
        if (enemies.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    return enemy;
                }
            }
            return null;
        }
    }
    public void Add(CharactersBase characters)
    {
        if (characters is PlayerCharacters)
        {
            this.characters.Add(characters as PlayerCharacters);
        }
        else if (characters is EnemyCharacters)
        {
            enemies.Add(characters as EnemyCharacters);
        }
    }
    public void Remove(CharactersBase characters)
    {

        if (characters is PlayerCharacters)
        {
            this.characters.Remove(characters as PlayerCharacters);
        }
        else if (characters is EnemyCharacters)
        {
            enemies.Remove(characters as EnemyCharacters);
        }

        Destroy(characters.gameObject);
    }
}
