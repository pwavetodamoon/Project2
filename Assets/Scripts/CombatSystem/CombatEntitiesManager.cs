using NewCombat.Characters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Helper;
using UnityEngine;

namespace CombatSystem
{
    public class CombatEntitiesManager : Singleton<CombatEntitiesManager>
    {
        [ShowInInspector] private Dictionary<string, List<GameObject>> entitiesByTag = new Dictionary<string, List<GameObject>>();

        protected override void Awake()
        {
            base.Awake();
            GetAllEntityInWorld();
        }

        public List<GameObject> GetHeroList()
        {
            return entitiesByTag[GameTag.Hero];
        }

        private GameObject GetNearestObject(Transform entity, List<GameObject> entities)
        {

            var minDistance = entityDistance(entity, entities[0]);
            GameObject nearestGameObject = entities[0];

            foreach (var entityInList in entities)
            {
                float newDistance;
                if ((newDistance = entityDistance(entity, entityInList)) > minDistance) continue;

                minDistance = newDistance;
                nearestGameObject = entityInList;
            }
            return nearestGameObject;


            float entityDistance(Transform entity, GameObject gameObject)
            {
                return Vector2.Distance(entity.transform.position, gameObject.transform.position);
            }
        }
        public GameObject GetEntityTransformByTag(Transform entity, string key)
        {
            // far attack
            if (IsContainKey(key) == false)
            {
                Debug.LogWarning($"Does not have '{key}' TAG in data", gameObject);
                return null;
            }
            var entities = GetList(key);

            if (entities.Count == 0) { return null; }

            return GetNearestObject(entity, entities);
        }
        private bool IsContainKey(string tag)
        {
            return entitiesByTag.ContainsKey(tag);
        }
        
        public void RemoveEntityByTag(GameObject entity, string key)
        {
            if (IsContainKey(key) && IsContainValue(key, entity))
            {
                entitiesByTag[key].Remove(entity);
            }
        }

        public bool IsHaveEntityHaveTagAlive(string tag)
        {
            if (IsContainKey(tag) == false) return false;
            return entitiesByTag[tag].Count != 0;
        }
        private List<GameObject> GetList(string key)
        {
            var list = IsContainKey(key) ? entitiesByTag[key] : null;
            return list;
        }

        public void AppendEntityToListByTag(GameObject entity, string key)
        {
            if (!IsContainKey(key))
            {
                entitiesByTag[key] = new List<GameObject>();
            }

            if (!IsContainValue(key, entity))
            {
                entitiesByTag[key].Add(entity);
            }
        }
        private bool IsContainValue(string key, GameObject value)
        {
            return entitiesByTag[key].Contains(value);
        }

  
        [Button]
        void GetAllEntityInWorld()
        {
            var newEntities = FindObjectsOfType<EntityCharacter>().ToList();
            foreach (var entity in newEntities)
            {
                Debug.Log($"Add {entity.name} to {entity.tag}");
                AppendEntityToListByTag(entity.gameObject, entity.tag);
            }
        }
    }
}