using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCollectBase : MonoBehaviour, IPointerEnterHandler
{
    public string Id;
    public float speed = 1;
    [SerializeField] private float yPosition;
    [SerializeField] private float time = .5f;
    private Vector2 originalPosition;
    private Vector2 originalScale;
    [SerializeField] private Ease ease;
    [SerializeField] new Collider2D collider2D;
    [Button(ButtonSizes.Medium)]
    public bool isCollected = false;
    void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void CollectEffect()
    {
        if (isCollected) return;
        collider2D.enabled = false;
        isCollected = true;

        originalPosition = transform.position;
        originalScale = transform.localScale;

        var newPosition = transform.position.y + yPosition;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(originalScale * 1.5f, time).SetEase(ease));
        sequence.Append(transform.DOMoveY(newPosition, time).SetEase(ease));
        sequence.Play().OnComplete(() =>
        {
            Collect();
        });
    }

    protected virtual void Collect()
    {
        Debug.Log("Item Collected: " + Id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CollectEffect();
    }

    [Button(ButtonSizes.Medium)]
    private void ResetPosition()
    {
        transform.position = originalPosition;
        transform.localScale = originalScale;
    }
}