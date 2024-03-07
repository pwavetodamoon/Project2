using System.Collections.Generic;
using System.Linq;
using Helper;
using NewCombat.Entity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem
{
    public class CombatEntitiesManager : Singleton<CombatEntitiesManager>
    {
        [SerializeField] private MonsterSpawner monsterSpawner;
        [ShowInInspector] private Dictionary<string, List<EntityCharacter>> entitiesByTag = new();

        protected override void Awake()
        {
            base.Awake();
            GetAllEntityInWorld();
            monsterSpawner = GetComponentInChildren<MonsterSpawner>();
        }

        public List<EntityCharacter> GetHeroList()
        {
            return entitiesByTag[GameTag.Hero];
        }

        public int GetHeroCount()
        {
            return entitiesByTag[GameTag.Hero].Count;
        }

        private EntityCharacter GetNearestObject(Transform entity, List<EntityCharacter> entities)
        {
            var minDistance = entityDistance(entity, entities[0]);
            var nearestGameObject = entities[0];

            foreach (var entityInList in entities)
            {
                float newDistance;
                if ((newDistance = entityDistance(entity, entityInList)) > minDistance) continue;

                minDistance = newDistance;
                nearestGameObject = entityInList;
            }

            return nearestGameObject;


            float entityDistance(Transform entity, EntityCharacter gameObject)
            {
                return Vector2.Distance(entity.transform.position, gameObject.transform.position);
            }
        }

        public EntityCharacter GetEntityTransformByTag(Transform entity, string key)
        {
            // far attack
            if (IsContainKey(key) == false)
            {
                Debug.LogWarning($"Does not have '{key}' TAG in data", gameObject);
                return null;
            }

            var entities = GetListInCombat(key);

            if (entities.Count == 0) return null;

            return GetNearestObject(entity, entities);
        }

        private bool IsContainKey(string tag)
        {
            return entitiesByTag.ContainsKey(tag);
        }

        public void RemoveEntityByTag(EntityCharacter entity, string key)
        {
            if (IsContainKey(key) && IsContainValue(key, entity)) entitiesByTag[key].Remove(entity);
        }

        public bool IsHaveEntityHaveTagAlive(string tag)
        {
            if (IsContainKey(tag) == false) return false;
            return entitiesByTag[tag].Count != 0;
        }

        public List<EntityCharacter> GetListInCombat(string key)
        {
            var list = IsContainKey(key) ? entitiesByTag[key] : null;
            return list;
        }


        public void AppendEntityToListByTag(EntityCharacter entity, string key)
        {
            if (!IsContainKey(key)) entitiesByTag[key] = new List<EntityCharacter>();

            if (!IsContainValue(key, entity)) entitiesByTag[key].Add(entity);
        }

        private bool IsContainValue(string key, EntityCharacter value)
        {
            return entitiesByTag[key].Contains(value);
        }


        [Button]
        private void GetAllEntityInWorld()
        {
            var newEntities = FindObjectsOfType<EntityCharacter>().ToList();
            foreach (var entity in newEntities)
            {
                Debug.Log($"Add {entity.name} to {entity.tag}");
                AppendEntityToListByTag(entity, entity.tag);
            }
        }
    }
}