using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggDropScript : MonoBehaviour
{
    private bool dragging, placed;
    private Vector2 offset, originalPos;
    public SpriteRenderer[] sprite;

    public SlotList[] slotList;
    public Transform SlotPos;
    void Awake()
    {

    }
    private void Start()
    {
        
        GetSpriteRenderer(false);
        sprite = GetComponentsInChildren<SpriteRenderer>();

    }
    void Update()
    {
        
        GetSlot();
        if (!dragging || placed) return;
        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
        originalPos = transform.parent.position;
    }
    private void OnMouseDown()
    {
        GetSpriteRenderer(true);
        dragging = true;
        offset = GetMousePos() - (Vector2)transform.position;

    }
    private void OnMouseUp()
    {
        GetSpriteRenderer(false);
        if (Vector2.Distance(transform.position, SlotPos.position) <= 3)
        {
            
            transform.parent.position = SlotPos.position;
            transform.position = transform.parent.position;
        }
        else
        {
            transform.position = originalPos;
        }
        dragging = false;

    }

    Vector2 GetMousePos()
    {
        return Camera.
            main.ScreenToWorldPoint(Input.mousePosition);

    }
    void GetSpriteRenderer(bool T)
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            sprite[i].enabled = T;

        }
    }
    void GetSlot()
    {
        for(int i = 0;i< slotList.Length;i++) 
        {
            if (Vector2.Distance(transform.position, slotList[i].transform.position) <= 3)
            { 
                SlotPos = slotList[i].transform;
                
            }
        }
    }

}
