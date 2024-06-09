using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Canvas�ݒ��Overlay�ɂ���

// Image�R���|�[�l���g��K�v�Ƃ���
[RequireComponent(typeof(Image))]

// �h���b�O�ƃh���b�v�Ɋւ���C���^�[�t�F�[�X����������
public class DragObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    // �h���b�O�O�̈ʒu
    private Vector2 prevPos;
    private bool isAttach = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        isAttach = false;
        // �h���b�O�O�̈ʒu���L�����Ă���
        prevPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �h���b�O���͈ʒu���X�V����
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAttach) return;
        // �h���b�O�O�̈ʒu�ɖ߂�
        transform.position = prevPos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            // ���� DroppableField �̏�Ȃ�A���̈ʒu�ɌŒ肷��
            if (hit.gameObject.CompareTag("DroppableField"))
            {
                transform.position = hit.gameObject.transform.position;
                isAttach = true;
                return;
            }
        }
    }
}