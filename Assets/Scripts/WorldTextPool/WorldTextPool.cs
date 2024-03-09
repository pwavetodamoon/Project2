using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorldTextPool
{
    public partial class WorldTextPool : Singleton<WorldTextPool>
    {

        [SerializeField] private int maxPoolSize = 20;
        [FormerlySerializedAs("textPrefab")]
        [Header("Settings of text")]
        [SerializeField] private BaseWorldText combatTxtPrefab;
        [SerializeField] TextPoolCustom combatTextPoolCustom;
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
        public BaseWorldText GetText(Vector2 position, string str,Color color)
        {
            var text = GetTxtFromPool(position, str);
            text.SetColor(color);
            return text;
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