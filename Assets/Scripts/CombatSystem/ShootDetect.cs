using UnityEngine;

public class ShootDetect : MonoBehaviour
{
    public bool ShootDone;
    public Transform BowAttackTransform;
    public Transform MagicAttackTransform;

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