using System;
using System.Collections;
using DG.Tweening;
using Helper;
using Item.ItemConfig;
using ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Item
{
    [Serializable]
    public abstract class BaseDrop : MonoBehaviour, IPointerEnterHandler, IPooled<BaseDrop>
    {
        public string itemID;
        public int point;
        [SerializeField][Required] private ItemDropConfig DropConfig;
        private Collider2D collider2d;
        private Transform DestinationTransform;
        private SpriteRenderer spriteRenderer;
        public Action<BaseDrop> ReleaseCallback { get; set; }


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
            collider2d.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.ITEMS);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.TriggerEvent)) Collect();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Collect();
            //Debug.Log("Trigger mouse");
        }


        public void Release()
        {
            ReleaseCallback?.Invoke(this);
        }

        public abstract void SendData();

        public void SetSprite(Sprite sprite)
        {
            if (spriteRenderer == null || sprite == null) return;
            spriteRenderer.sprite = sprite;
        }

        [Button]
        public virtual void Collect()
        {
            StartCoroutine(MoveToDestination());
            AudioManager.Instance.PlaySFX("Collect Item");
        }

        private IEnumerator MoveToDestination()
        {
            collider2d.enabled = false;

            if (DestinationTransform != null)
            {
                var time = Random.Range(.5f, 1f);
                yield return transform.DOLocalMove(DestinationTransform.position, time).WaitForCompletion();
            }
            SendData();
            Release();
        }

        public void Jumping(Vector3 position)
        {
            transform.DORotate(CreateRandom(), 0.2f);
            if (DropConfig == null)
                return;
            transform.DOLocalJump(position,
                    DropConfig.jumpForce,
                    DropConfig.jumpCount,
                    DropConfig.duration)
                .OnComplete(() =>
                {
                    Collect();
                    // collider2d.enabled = true;
                });
        }
        private Vector3 CreateRandom()
        {
            return new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f));
        }
        public void SetDestination(Transform destination)
        {
            DestinationTransform = destination;
        }
    }
}