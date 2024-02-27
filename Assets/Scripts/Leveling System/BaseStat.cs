using System;
using UnityEngine;

namespace Leveling_System
{
    [Serializable]
    public class BaseStat
    {
        [SerializeField] private float value;
        private float modValue;
        public BaseStat(float value)
        {
            Value = value;
        }
        public float Value
        {
            get
            {
                return Mathf.Clamp(value + modValue, 0, float.MaxValue);
            }
            set
            {
                this.value = value;
            }
        }


    }

}