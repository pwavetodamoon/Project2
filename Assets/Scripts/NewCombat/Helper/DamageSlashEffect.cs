using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace NewCombat.Helper
{
    public class DamageSlashEffect : MonoBehaviour
    {
        private Material defaultMaterial;
        [SerializeField, Required] private Material slashMaterial;
        private SpriteRenderer[] spriteRenderers;
        private float slashDuration = 0.1f;
        private float slashTimer = 0f;
        private bool isSlashing = false;

        private void Awake()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            if (spriteRenderers.Length > 0)
                defaultMaterial = spriteRenderers[0].material;
        }

        public void TriggerFlashEffect()
        {
            if (slashMaterial == null || defaultMaterial == null) return;

            SetSlashMaterial();
            slashTimer = 0f;
            StartCoroutine(SlashEffect());
        }

        private IEnumerator SlashEffect()
        {
            if (isSlashing) yield break;
            isSlashing = true;
            while (slashTimer <= slashDuration)
            {
                slashTimer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            isSlashing = false;
            SetDefaultMaterial();
        }

        private void SetSlashMaterial()
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.material = slashMaterial;
            }
        }

        private void SetDefaultMaterial()
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.material = defaultMaterial;
            }
        }
    }
}