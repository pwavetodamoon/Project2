using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVFX : MonoBehaviour
{
    public ParticleSystem particleSystems;
    public void Play()
    {
        if (particleSystems == null) return;
        var go = Instantiate(particleSystems, transform.position, Quaternion.identity);
        go.Play();
    }
}
