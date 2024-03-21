using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSwap : MonoBehaviour
{
    public Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Trigger()
    {
        animator.SetBool("Open", true);
    }
    public void Reset()
    {
        animator.SetBool("Open", false);
    }
}
