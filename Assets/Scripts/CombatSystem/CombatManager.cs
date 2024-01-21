using NewCombat.Characters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatSystem
{
    public class CombatManager : MonoBehaviour
    {
        public List<HeroCharacter> Hero;
        public List<MonsterCharacter> Monster;
        public static CombatManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public MonsterCharacter GetMonster()
        {
            if (Monster.Count == 0) return null;
            
            int index = 0;
            if (Monster[index] == null)
            {
                Monster.RemoveAt(index);
            }
            return Monster[index];
        }
        public void AddMonster(MonsterCharacter monsterCharacter)
        {
            Monster.Add(monsterCharacter);
        }
        public HeroCharacter GetHero()
        {
            if (Hero.Count == 0) return null;
            return Hero[0];
        }
        [Button]
        void GetAllCharacter()
        {
            Hero = FindObjectsOfType<HeroCharacter>().ToList();
            foreach (HeroCharacter character in Hero)
            {
                Debug.Log(character.name);
            }
            Monster = FindObjectsOfType<MonsterCharacter>().ToList();
            foreach (MonsterCharacter character in Monster)
            {
                Debug.Log(character.name);
            }
        }
    }
}