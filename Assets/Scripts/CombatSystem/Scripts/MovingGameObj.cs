using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGameObj : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Transform target;
    public int rotate = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Moving()    
    {

        Vector3 dir = target.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;

        float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + rotate);
    }
}
