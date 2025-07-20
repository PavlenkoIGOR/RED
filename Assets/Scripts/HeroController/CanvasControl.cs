using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] Hero _hero;
    public RectTransform rectTransform;
    private float canvasWidth;
    float deltaX;
    private Vector3 previousPosition;
    void Start()
    {       
        if (_hero != null)
        {
            canvasWidth = rectTransform.rect.width * rectTransform.lossyScale.x;
            previousPosition = _hero.transform.position;
        }
    }
    void Update()
    {
        if (_hero != null)
        {
            deltaX = _hero.transform.position.x - previousPosition.x;
            if (deltaX < 0 && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 <= GameController.screenLeft
                ||
                deltaX > 0 && transform.position.x + rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenRight)
            {               
                print($"out {deltaX}");
                transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);                   
            }

            if (deltaX > 0 && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenLeft)
            {
                print($"out 2 {deltaX}");
                if (deltaX < previousPosition.x - transform.position.x)
                {
                    transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
                }
            }

            previousPosition = _hero.transform.position;
        }

    }
}
