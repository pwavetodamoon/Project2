using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // TODO: Combat manager do to much work, need to refactor
    [SerializeField] private List<PlayerCharacters> characters = new List<PlayerCharacters>();

    [SerializeField] private List<EnemyCharacters> enemies = new List<EnemyCharacters>();

    public static Func<int, CharactersBase> GetEnemyPosition;
    public static Action<CharactersBase> RemoveCharacter;

    private static List<Action> actionsListPlayer = new List<Action>();
    private static List<Action> actionsListMonster = new List<Action>();

    private void Awake()
    {
        GetEnemyPosition += GetEnemy;
        RemoveCharacter += Remove;
    }

    private void OnDisable()
    {
        GetEnemyPosition -= GetEnemy;
        RemoveCharacter -= Remove;
    }

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
        //StartCoroutine(CheckingReapting());
    }

    private IEnumerator CheckingReapting()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            CheckAttack(actionsListPlayer, enemies.Count);
            //CheckAttack(actionsListPlayer, character.Count);
        }
    }

    public void ClearActionList()
    {
        actionsListMonster.Clear();
        actionsListPlayer.Clear();
    }

    private void CheckAttack(List<Action> actionsList, int count)
    {
        if (actionsList.Count > 0 && count > 0)
        {
            foreach (var action in actionsList)
            {
                //Debug.Log("Attack Callback");
                action?.Invoke();
                //actionsList.Remove(CallbackMethod);
            }
            actionsList.Clear();
        }
    }

    // TODO: Make method for get enemy of player
    private CharactersBase GetEnemy(int state)
    {
        if (state == 0)
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
            }
        }
        else
        {
            if (characters.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var character in characters)
                {
                    if (character != null)
                    {
                        return character;
                    }
                }
            }
        }
        return null;
    }

    public void Add(CharactersBase character)
    {
        if (character is PlayerCharacters)
        {
            this.characters.Add(character as PlayerCharacters);
        }
        else if (character is EnemyCharacters)
        {
            enemies.Add(character as EnemyCharacters);
        }
    }

    public void Remove(CharactersBase character)
    {
        if (character is PlayerCharacters)
        {
            character.gameObject.SetActive(false);
            this.characters.Remove(character as PlayerCharacters);
        }
        else if (character is EnemyCharacters)
        {
            character.gameObject.SetActive(false);
            enemies.Remove(character as EnemyCharacters);
        }
        Destroy(character.gameObject);
    }
}