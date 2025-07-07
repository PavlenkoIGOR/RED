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
            Vector2 size = rectTransform.rect.size; // Используем rect.size вместо sizeDelta

            // Предположим, что хотим нормализовать в диапазон [-1, 1]
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
        // Внутри метода Update или другого метода
        Vector3 startPoint = localPoint; // или любая точка
        Vector3 direction = localPoint - (Vector2)_hero.transform.position; // пример вектора
        float length = 5f; // длина линии

        // Нарисовать линию из startPoint в направлении direction
        Debug.DrawLine(_hero.transform.position, startPoint, Color.red);
    }
}
