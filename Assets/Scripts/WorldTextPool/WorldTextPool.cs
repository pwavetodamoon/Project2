using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorldText
{
    public partial class WorldTextPool : Singleton<WorldTextPool>
    {
        public enum TextType
        {
            Coin,
            Combat
        }

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
        private void Test(TextType type)
        {
            GetText(Vector2.zero, "Test");
        }
        public void GetText(Vector2 position, string str,Color color)
        {
            var text = GetTxtFromPool(position, str);
            text.SetColor(color);
        }

        public void GetText(Vector2 position, string str)
        {
            var txt = GetTxtFromPool(position, str);
            txt.SetColor(Color.red);
        }
        private BaseWorldText GetTxtFromPool(Vector2 position, string str)
        {
            var textMeshPro = combatTextPoolCustom.Get();
            textMeshPro.transform.localPosition =
                position;/*+ combatTextPoolCustom.GetRandomPosition();*/

            textMeshPro.Text = str;
            textMeshPro.Init();
            return textMeshPro;
        }
    }
}