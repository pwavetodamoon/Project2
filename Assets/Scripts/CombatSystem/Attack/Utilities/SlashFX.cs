using DG.Tweening;
using UnityEngine;

namespace CombatSystem.Attack.Utilities
{
    public class SlashFX : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer.gameObject.SetActive(false);
        }

        public void ShowFx()
        {
            spriteRenderer.DOFade(1, 0);
            spriteRenderer.gameObject.SetActive(true);
            spriteRenderer.DOFade(0, 0.08f).OnComplete(() => spriteRenderer.gameObject.SetActive(false));
        }
    }
}