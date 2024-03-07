using System;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldText
{
    public partial class WorldTextPool
    {
        [Serializable]
        private class TextPoolCustom
        {
            [SerializeField] private Vector2 randomPositionRangeX = new(-1, 1);
            [SerializeField] private Vector2 randomPositionRangeY = new(-1, 1);
            [SerializeField] private int fontSize = 5;
            public int maxPool = 20;

            private ObjectPoolPrefab<BaseWorldText> objectPoolPrefab;
            private BaseWorldText textPrefab;

            public TextPoolCustom(BaseWorldText prefab, Transform parent, int maxPool)
            {
                if (prefab == null)
                {
                    Debug.LogError("Prefab is null");
                    return;
                }

                textPrefab = prefab;
                this.maxPool = maxPool;
                objectPoolPrefab = new ObjectPoolPrefab<BaseWorldText>(textPrefab, parent, maxPool);
            }

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