using UnityEngine;

namespace SlotHero
{
    public class ShadowColor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer ShadowSprite;
        public Color normalColor;
        public Color onChooseColor;
        private bool IsChange = false;

        private void Awake()
        {
            ShadowSprite.color = normalColor;
        }

        public void SetOriginal()
        {
            ShadowSprite.color = normalColor;
        }

        public void SetOnChoose()
        {
            ShadowSprite.color = onChooseColor;
        }
    }
}