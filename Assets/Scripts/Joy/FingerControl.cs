using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FingerControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Hero _hero;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _rectTransform;
    public static Vector2 Value { get; set; }
    Vector2 dir;

    Vector2 localPoint;
    public void OnDrag(PointerEventData eventData)
    {
        print($"value {Value}");
        OnPointerDown(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Получаем локальные координаты внутри RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, Camera.main, out localPoint);

        // Преобразуем локальные координаты в мировые
        Vector3 worldPos = _rectTransform.TransformPoint(localPoint);
        Debug.Log($"heroPos {_hero.transform.position}");
        Debug.Log($"pointPos {worldPos}");
        // Проверяем попадание в Collider2D
        if (_hero._heroCollider.OverlapPoint(new Vector2(worldPos.x, worldPos.y)))
        {
            Debug.Log("inn");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector2.zero;
        dir = Vector2.zero;
    }
}
