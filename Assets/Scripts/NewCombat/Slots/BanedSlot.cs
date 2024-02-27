using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanedSlot : MonoBehaviour
{
    public float radius = .5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
