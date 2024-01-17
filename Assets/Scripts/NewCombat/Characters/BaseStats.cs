using Sirenix.OdinInspector;
using UnityEngine;

namespace NewCombat.Characters
{
    public class BaseStats : MonoBehaviour
    {
        public float Health = 100;
        public float Dex = 0;
        public float Strength = 1;

        [Button]
        public void ResetState()
        {
            Health = 100;
        }
    }
}