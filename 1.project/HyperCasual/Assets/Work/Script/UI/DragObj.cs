using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Canvas設定をOverlayにする

// Imageコンポーネントを必要とする
[RequireComponent(typeof(Image))]

// ドラッグとドロップに関するインターフェースを実装する
public class DragObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    // ドラッグ前の位置
    private Vector2 prevPos;
    private bool isAttach = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        isAttach = false;
        // ドラッグ前の位置を記憶しておく
        prevPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中は位置を更新する
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAttach) return;
        // ドラッグ前の位置に戻す
        transform.position = prevPos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            // もし DroppableField の上なら、その位置に固定する
            if (hit.gameObject.CompareTag("DroppableField"))
            {
                transform.position = hit.gameObject.transform.position;
                isAttach = true;
                return;
            }
        }
    }
}