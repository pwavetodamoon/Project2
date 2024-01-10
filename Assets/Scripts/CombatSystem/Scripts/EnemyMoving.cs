using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoving : MonoBehaviour
{
    public float speed;
    public bool isMoving = true;

    public void Setup(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        Moving();
    }

    public void Moving()
    {
        if (!isMoving) return;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}