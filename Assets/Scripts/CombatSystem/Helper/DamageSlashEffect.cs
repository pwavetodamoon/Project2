using CombatSystem.Entity.Utilities;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace CombatSystem.Helper
{
    public class DamageSlashEffect : MonoBehaviour
    {
        [SerializeField][Required] private Material slashMaterial;
        private readonly float slashDuration = 0.1f;
        private readonly WaitForEndOfFrame waitForEndOfFrame = new();
        private float slashTimer;
        private Material defaultMaterial;
        private bool isSlashing;
        private SpriteRenderer[] spriteRenderers;
        private EntityTakeDamage EntityStateManager;

        public Transform modelTransform;
        public Transform parent;

        private void Awake()
        {
            parent = transform.parent;
            spriteRenderers = modelTransform.GetComponentsInChildren<SpriteRenderer>();
            EntityStateManager = parent.GetComponentInChildren<EntityTakeDamage>();
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