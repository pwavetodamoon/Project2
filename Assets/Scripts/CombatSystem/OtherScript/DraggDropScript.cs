using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggDropScript : MonoBehaviour
{
    private bool dragging, placed;
    private Vector2 offset, originalPos;
    private SpriteRenderer sprite;

    public SlotScript slot;
    void Awake()
    {
        originalPos = transform.position;
    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!dragging || placed) return;
        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }
    private void OnMouseDown()
    {
        dragging = true;
        offset = GetMousePos() - (Vector2)transform.position;
        sprite.enabled = true;
    }
    private void OnMouseUp()
    {
        sprite.enabled = false;
        if (Vector2.Distance(transform.position, slot.transform.position) <= 3)
        {
            transform.position = slot.transform.position;
        }
        else
        {
            transform.position = originalPos;
            dragging = false;
        }
    }
    Vector2 GetMousePos()
    {
        return Camera.
            main.ScreenToWorldPoint(Input.mousePosition);

    }
}
