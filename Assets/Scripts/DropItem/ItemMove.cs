using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 moveVector = Vector2.left;
    public void Update()
    {
        transform.Translate(moveVector * (Time.deltaTime * speed));
    }
}

