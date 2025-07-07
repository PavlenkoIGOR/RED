using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FingerControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Hero _hero;
    public static Vector2 Value { get; set; }
    Vector2 localPoint;
    public void OnDrag(PointerEventData eventData)
    {
        print($"value {Value}");
        OnPointerDown(eventData);
        DrawVector();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            Vector2 size = rectTransform.rect.size; // ���������� rect.size ������ sizeDelta

            // �����������, ��� ����� ������������� � �������� [-1, 1]
            float normalizedX = localPoint.x / size.x;
            float normalizedY = localPoint.y / size.y;

            Value = new Vector2(normalizedX, normalizedY);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector2.zero;
    }

    void DrawVector()
    {
        // ������ ������ Update ��� ������� ������
        Vector3 startPoint = localPoint; // ��� ����� �����
        Vector3 direction = localPoint - (Vector2)_hero.transform.position; // ������ �������
        float length = 5f; // ����� �����

        // ���������� ����� �� startPoint � ����������� direction
        Debug.DrawLine(_hero.transform.position, startPoint, Color.red);
    }
}
