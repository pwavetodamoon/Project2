using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnScreen : MonoBehaviour, IPointerEnterHandler
{
    public string Id;

    [SerializeField] private float yPosition;
    [SerializeField] private float time = .5f;
    private Vector2 originalPosition;
    private Vector2 originalScale;
    [SerializeField] private Ease ease;

    [Button(ButtonSizes.Medium)]
    public bool isCollected = false;

    public void CollectEffect()
    {
        isCollected = true;
        originalPosition = transform.position;
        originalScale = transform.localScale;

        var newPosition = transform.position.y + yPosition;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.one * .5f, time).SetEase(ease));
        sequence.Append(transform.DOMoveY(newPosition, time).SetEase(ease));
        sequence.Play().OnComplete(() =>
        {
            Collect();
        });
    }

    public void Collect()
    {
        Debug.Log("Item Collected: " + Id);
        QuestingSystem.Instance.CollectItem(Id);
        transform.gameObject.SetActive(false);
        // Destroy(gameObject, .1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isCollected) return;
        CollectEffect();
    }

    [Button(ButtonSizes.Medium)]
    private void ResetPosition()
    {
        transform.position = originalPosition;
        transform.localScale = originalScale;
    }
}