using CombatSystem.Entity;
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
        private bool isSlashing;
        private Material defaultMaterial;
        private SpriteRenderer[] spriteRenderers;
        private EntityAction entityAction;

        public Transform modelTransform;
        public Transform parent;

        private void Start()
        {
            parent = transform.parent;
            spriteRenderers = modelTransform.GetComponentsInChildren<SpriteRenderer>();
            entityAction = GetComponentInParent<EntityCharacter>().GetRef<EntityAction>();
            if (spriteRenderers.Length > 0)
                defaultMaterial = spriteRenderers[0].material;
            entityAction.OnTakeDamage += TriggerFlashEffect;
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