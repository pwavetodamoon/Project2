using Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorldTextPool
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

        [SerializeField] private BaseWorldText coinTxtPrefab;

        [SerializeField] TextPoolCustom combatTextPoolCustom;
        [SerializeField] TextPoolCustom coinTextPoolCustom;
        protected override void Awake()
        {
            base.Awake();
            combatTextPoolCustom = new TextPoolCustom
                (combatTxtPrefab, transform, maxPoolSize);
            coinTextPoolCustom = new TextPoolCustom
                (coinTxtPrefab, transform, maxPoolSize);
        }

        [Button]
        private void Test(TextType type)
        {
            if (type == TextType.Coin)
                GetCoinTxt(Vector2.zero, "Test");
            else
                GetCombatTxt(Vector2.zero, "Test");
        }

        public void GetCombatTxt(Vector2 position, string str)
        {
            GetTxtFromPool(position, str, combatTextPoolCustom);
        }

        public void GetCoinTxt(Vector2 position, string str)
        {
            GetTxtFromPool(position, str, coinTextPoolCustom);
        }
        private void GetTxtFromPool(Vector2 position, string str, TextPoolCustom combatTextPoolCustom)
        {
            var textMeshPro = combatTextPoolCustom.Get();
            textMeshPro.transform.localPosition =
                position;/*+ combatTextPoolCustom.GetRandomPosition();*/

            textMeshPro.Text = str;
            textMeshPro.Init();
        }
    }
}