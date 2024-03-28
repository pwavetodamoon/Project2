using System;
using UnityEngine;

[Serializable]
public class Cooldown 
{
    public float NextActionTime;
    public float CoolDownTime;
    public float RemainingCooldownTime => NextActionTime - Time.time;
    public bool IsCoolingDown => Time.time < NextActionTime;
    public void StartCooldown() => NextActionTime = Time.time + CoolDownTime;
}