using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkill : MonoBehaviour
{
    public GameObject Enemy;
    public Animator animator;

    public float damage;
    public Vector2 size = Vector2.one;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.transform.position, size);
    }
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public virtual void DealDamage()
    {

    }
    public void Destroy()
    {
        Destroy(gameObject);
        // transform.gameObject.SetActive(false);
    }
}
