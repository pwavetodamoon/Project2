using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private new Rigidbody2D rigidbody2D;
    public Transform shooter;
    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
    }
    public void SetVelocity(Vector2 direction, float speed = 2)
    {
        rigidbody2D.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //circleCollider2D.enabled = false;
        Debug.Log(collision.name);
        var circles = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);
        foreach (var circle in circles)
        {
            if (circle.TryGetComponent(out MonsterCharacter monsterCharacter))
            {
                Debug.Log(shooter+" is hit: "+monsterCharacter.name);
            }
        }
    }
}
