using System.Collections;
using System.Collections.Generic;
using CombatSystem.Attack.Utilities;
using Helper;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public GameObject Enemy;
    public Animator animator;

    public float damage;
    public Vector2 size;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.transform.position, size);
    }
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void DealDamage()
    {
        var collider = Physics2D.OverlapBox(transform.transform.position, size, 0, GameLayerMask.Get(GameLayerMask.ENEMY));
        if (collider != null)
        {
            collider.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Debug.Log("Fire Attack");
    }
    public void Destroy()
    {
        Destroy(gameObject);
        // transform.gameObject.SetActive(false);
    }
}
