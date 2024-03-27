using UnityEngine;

namespace SlotHero
{
    public class ShadowColor : MonoBehaviour
    {
        public ParticleSystem ParticleSystem;
        private void Awake()
        {
            ParticleSystem.Stop();
        }

    }
}