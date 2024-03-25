using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDetect : MonoBehaviour
{
    public bool ShootDone;

    public void Shoot()
    {
        // Debug.Log("Shoot");
        ShootDone = true;
    }
    public void ResetShoot()
    {
        // Debug.Log("Reset Shoot");
        ShootDone = false;
    }
}
