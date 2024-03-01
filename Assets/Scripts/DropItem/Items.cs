using System;
using System.Collections;
using DG.Tweening;
using DropItem.ItemConfig;
using Helper;
using ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace DropItem
{
    [Serializable]
    public abstract class Items : MonoBehaviour, IPointerEnterHandler, IPooled<Items>
    {
        private Collider2D collider2d;
        private SpriteRenderer spriteRenderer;
        private Transform DestinationTransform;
        public string itemID;
        public int point;
        public Action<Items> ReleaseCallback { get; set; }
        public abstract void SendData();




        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
            collider2d.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer(GameLayerMask.Items);
        }

        public void SetSprite(Sprite sprite)
        {
            if (spriteRenderer == null || sprite == null) return;
            spriteRenderer.sprite = sprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTag.TriggerEvent))
            {
                Collect();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Collect();
            Debug.Log("Trigger mouse");
        }
        
        [Button]
        public virtual void Collect()
        {
            Debug.Log("check Collect");
            StartCoroutine(MoveToDestination());

        }

        IEnumerator MoveToDestination()
        {
            collider2d.enabled = false;
            
            if (DestinationTransform != null)
            {
                yield return transform.DOLocalMove(DestinationTransform.position, 1f).WaitForCompletion();
            }
            SendData();
            Release();
            Debug.Log("check MoveToDestination");
        }
        [SerializeField, Required] private ItemDropConfig DropConfig;

        public void Pump(Vector3 position)
        {
            if (DropConfig == null)
                return;
            transform.DOLocalJump(position,
                DropConfig.jumpForce,
                DropConfig.jumpCount,
                DropConfig.duration)
                .OnComplete(() =>
            {
                collider2d.enabled = true;
            });
        }

        public void Release()
        {
            ReleaseCallback?.Invoke(this);
        }
    }
}
