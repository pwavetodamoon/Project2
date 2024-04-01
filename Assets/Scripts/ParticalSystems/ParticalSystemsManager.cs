using System;
using System.Collections;
using System.Collections.Generic;
using Effects.Skill;
using Sirenix.OdinInspector;
using UnityEngine;
public class ParticalSystemsManager : MonoBehaviour
{
   [SerializeField] private List<ParticleSystem> _listParticalSystems;
   public void FindAndPlayEffect(EffectSkillsEnum effectSkillsEnum)
   {
      for (int i = 0; i < _listParticalSystems.Count; i++)
      {
         if (_listParticalSystems[i].name == effectSkillsEnum.ToString())
         {
            _listParticalSystems[i].Play();
            break;
         }
      }
   }
   [Button]
   public void Test()
   {
      FindAndPlayEffect(EffectSkillsEnum.HealthEffect);
   }
}
