using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] Transform CharacterStand;
    [SerializeField] Transform EnemyStand;
    [SerializeField] string CharacterStandName;
    [SerializeField] string EnemyStandname;
    [Button]
    private void LoadStand()
    {
        if(CharacterStand == null)
        {
            CharacterStand = transform.Find(CharacterStandName);
        }
        if(EnemyStand == null)
        {
            EnemyStand = transform.Find(EnemyStandname);
        }
    }
    public Vector3 GetCharacterPosition()
    {
        return CharacterStand.position;
    }
    public Vector3 GetAttackerPosition()
    {
        return EnemyStand.position;
    }
}
