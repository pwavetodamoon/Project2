using System;
using System.Collections;
using System.Collections.Generic;
using Helper;
using Sirenix.OdinInspector;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace WorldTextPool
{
    public partial class WorldTextPool : Singleton<WorldTextPool>
    {

        [SerializeField] private int maxPoolSize = 20;
        [FormerlySerializedAs("textPrefab")]
        [Header("Settings of text")]
        [SerializeField] private BaseWorldText combatTxtPrefab;
        [SerializeField] TextPoolCustom combatTextPoolCustom;
        private Queue<Action> queue = new Queue<Action>();
        public int callCount;
        public float timer;
        public float maxTimer;
        protected override void Awake()
        {
            base.Awake();
            combatTextPoolCustom = new TextPoolCustom(combatTxtPrefab, transform, maxPoolSize);
        }

        [Button]
        private void DebugFunction()
        {
            GetText(Vector2.zero, "Test", Color.white);
        }
        public BaseWorldText GetText(Vector2 position, string str, Color color)
        {
            queue.Enqueue(() =>
            {
                var text = GetTxtFromPool(position, str);
                text.SetColor(color);
            });
            callCount++;
            return null;
        }


        private void Update()
        {
            if (callCount > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = maxTimer;
                    callCount--;
                    queue.Dequeue()?.Invoke();
                }
            }
        }
        private BaseWorldText GetTxtFromPool(Vector2 position, string str)
        {
            var textMeshPro = combatTextPoolCustom.Get();
            textMeshPro.transform.localPosition = position;
            textMeshPro.Text = str;
            textMeshPro.Init();
            return textMeshPro;
        }
    }
}