


using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class FingerControl : MonoBehaviour
{
    private Hero _hero;
    bool _moveAllowed = false;


    public RectTransform rectTransform; // Укажите ваш RectTransform

    public Sprite knobSprite;





    [SerializeField] TMP_Text testText;
    private Vector2 touchPosition;


    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Touchscreen.current != null && UnityEngine.InputSystem.Touchscreen.current.primaryTouch.press.isPressed)
        {
            var touch = UnityEngine.InputSystem.Touchscreen.current.primaryTouch;
            touchPosition = touch.position.ReadValue();

            Vector2 worldTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));
            UnityEngine.InputSystem.TouchPhase phase = touch.phase.ReadValue();

            switch (phase)
            {
                case UnityEngine.InputSystem.TouchPhase.Began:
                    // Проверка коллайдера при начале касания
                    Collider2D collider = Physics2D.OverlapCircle(worldTouchPos, 0.1f);
                    if (collider != null)
                    {
                        _hero = collider.transform.root.GetComponent<Hero>();
                        if (_hero != null)
                        {
                            _moveAllowed = true;
                        }
                    }
                    break;

                case UnityEngine.InputSystem.TouchPhase.Moved:
                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    if (_moveAllowed && _hero != null)
                    {
                        // Двигаем героя за текущую позицию касания
                        _hero.transform.position = worldTouchPos;
                    }
                    break;

                case UnityEngine.InputSystem.TouchPhase.Ended:
                case UnityEngine.InputSystem.TouchPhase.Canceled:
                    _moveAllowed = false;
                    break;
            }
        }
        else
        {
            // Если нет касания или оно отпущено, останавливаем движение
            _moveAllowed = false;
        }
    }
}
