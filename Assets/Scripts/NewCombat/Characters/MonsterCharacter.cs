using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharacter : MonoBehaviour
{
    [SerializeField] private Transform AttackedTransform;
    public Animator_Base Animator;
    private void Awake()
    {
        Animator = GetComponentInChildren<Animator_Base>();
    }
    public void Attack()
    {
    }
    public Vector3 GetAttackerPosition()
    {
        if(AttackedTransform == null)
        {
            return transform.position;
        }
        return AttackedTransform.position;
    }
}
