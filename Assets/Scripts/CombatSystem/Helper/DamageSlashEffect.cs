using System.Collections;
using CombatSystem.Entity.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Helper
{
    public class DamageSlashEffect : MonoBehaviour
    {
        [SerializeField] [Required] private Material slashMaterial;
        private readonly float slashDuration = 0.1f;
        private readonly WaitForEndOfFrame waitForEndOfFrame = new();
        private Material defaultMaterial;
        private bool isSlashing;
        private float slashTimer;
        private SpriteRenderer[] spriteRenderers;
        private EntityStateManager EntityStateManager;
        private void Awake()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            EntityStateManager = GetComponent<EntityStateManager>();
            if (spriteRenderers.Length > 0)
                defaultMaterial = spriteRenderers[0].material;
            EntityStateManager.OnTakeDamage += TriggerFlashEffect;
        }

        private void OnDisable()
        {
            EntityStateManager.OnTakeDamage -= TriggerFlashEffect;
        }

        public void TriggerFlashEffect()
        {
            if (slashMaterial == null || defaultMaterial == null) return;
            slashTimer = 0f;
            SetSlashMaterial();
            StartCoroutine(SlashEffect());
        }

        private IEnumerator SlashEffect()
        {
            if (isSlashing) yield break;
            isSlashing = true;
            while (slashTimer <= slashDuration)
            {
                slashTimer += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            isSlashing = false;
            SetDefaultMaterial();
        }

        private void SetSlashMaterial()
        {
            foreach (var spriteRenderer in spriteRenderers) spriteRenderer.material = slashMaterial;
        }

        private void SetDefaultMaterial()
        {
            foreach (var spriteRenderer in spriteRenderers) spriteRenderer.material = defaultMaterial;
        }
    }
}