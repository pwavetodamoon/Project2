using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CombatManager : MonoBehaviour
{
    // TODO: Combat manager do to much work, need to refactor
    [SerializeField] List<PlayerCharacters> characters = new List<PlayerCharacters>();
    [SerializeField] List<EnemyCharacters> enemies = new List<EnemyCharacters>();

    public static Func<EnemyCharacters> GetEnemyPosition;
    public static Action<CharactersBase> RemoveCharacter;

    static List<Action> actionsListPlayer = new List<Action>();
    static List<Action> actionsListMonster = new List<Action>();


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
        StartCoroutine(CheckingReapting());
    }
    IEnumerator CheckingReapting()
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
                //actionsList.Remove(action);
            }
            actionsList.Clear();
        }
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

    }

}
