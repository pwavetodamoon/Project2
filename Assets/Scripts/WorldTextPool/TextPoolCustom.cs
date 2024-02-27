using System;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldTextPool
{
    public partial class WorldTextPool
    {
        [Serializable]
        private class TextPoolCustom
        {
            public TextPoolCustom(BaseWorldText prefab, Transform parent, int maxPool)
            {
                if(prefab == null)
                {
                    Debug.LogError("Prefab is null");
                    return;
                }
                this.textPrefab = prefab;
                this.maxPool = maxPool;
                objectPoolPrefab = new ObjectPoolPrefab<BaseWorldText>(textPrefab, parent, maxPool);
            }
            [SerializeField] private Vector2 randomPositionRangeX = new Vector2(-1, 1);
            [SerializeField] private Vector2 randomPositionRangeY = new Vector2(-1, 1);
        
            private ObjectPoolPrefab<BaseWorldText> objectPoolPrefab;
            private BaseWorldText textPrefab;
            [SerializeField] private int fontSize = 5;
            public int maxPool = 20;
            public Vector2 GetRandomPosition()
            {
                return new Vector2(
                    Random.Range
                        (randomPositionRangeX.x, randomPositionRangeX.y),
                    Random.Range
                        (randomPositionRangeY.x, randomPositionRangeY.y));
            }

            public BaseWorldText Get()
            {
                return objectPoolPrefab.Get();
            }
        }
    }
}