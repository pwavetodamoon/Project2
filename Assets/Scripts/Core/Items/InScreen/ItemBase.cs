using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Collider2D))]
public abstract partial class ItemBase : MonoBehaviour, IPointerEnterHandler, ICollect
{
    public string Id;
    [SerializeField] private float yPosition;
    [SerializeField] private float time = .5f;
    //private Vector2 originalPosition;
    //private Vector2 originalScale;
    [SerializeField] private Ease ease;
    [SerializeField] new Collider2D collider2D;
    [Button(ButtonSizes.Medium)]
    public bool isCollected = false;
    void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }
    protected virtual void GatherEffect()
    {
        if (isCollected) return;
        if(collider2D == null) collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        isCollected = true;

        var originalPosition = transform.position;
        var originalScale = transform.localScale;

        var newPosition = transform.position.y + yPosition;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(originalScale * 1.5f, time).SetEase(ease));
        sequence.Append(transform.DOMoveY(newPosition, time).SetEase(ease));
        sequence.Play().OnComplete(
            () => GatherCallback());
    }

    public virtual void Gather()
    {
        Debug.Log("Item Collected: " + Id);
        GatherEffect();
    }
    protected virtual void GatherCallback()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Gather();
    }

}
public interface ICollect
{
    void Gather();
}