using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;

    //[SerializeField] private float minX;
    //[SerializeField] private float maxX;

    //[SerializeField] private float minY;
    //[SerializeField] private float maxY;

    public float minX { get; set; }
    public float maxX { get; set; }

    public float minY { get; set; }
    public float maxY { get; set; }

    private Vector3 targetPosition; // Целевая позиция в мире

    private void Start()
    {
        targetPosition = transform.position;

        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y;
        minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
    }
    private void Update()
    {
        Move();


        UpdateTargetPosition();
    }


    private void Move()
    {
        transform.Translate(VirtualJoystick.Value * speed * Time.deltaTime);

        var x = transform.position.x;
        var y = transform.position.y;

        var clampX = Mathf.Clamp(x, minX, maxX);
        var clampY = Mathf.Clamp(y, minY, maxY);
        transform.position = new Vector2(clampX, clampY);



        ////if (VirtualJoystick.isActive == true)
        ////{
        //    transform.Translate(FingerControl.Value * speed * Time.deltaTime);

        //    var x = transform.position.x;
        //    var y = transform.position.y;

        //    var clampX = Mathf.Clamp(x, minX, maxX);
        //    var clampY = Mathf.Clamp(y, minY, maxY);
        //    transform.position = new Vector2(clampX, clampY);
        ////}
    }
    private void UpdateTargetPosition()
    {
        // Получаем normalized координаты из FingerControl
        transform.Translate(VirtualJoystick.Value * speed * Time.deltaTime); // предполагается, что normalized в диапазоне [-1, 1]

        //var x = transform.position.x;
        //var y = transform.position.y;

        //var clampX = Mathf.Clamp(x, minX, maxX);
        //var clampY = Mathf.Clamp(y, minY, maxY);
        //transform.position = new Vector2(clampX, clampY);
    }
}








/*
 
         //if (VirtualJoystick.isActive == true)
        //{
        //    transform.Translate(VirtualJoystick.Value * speed * Time.deltaTime);

        //    var x = transform.position.x;
        //    var y = transform.position.y;

        //    var clampX = Mathf.Clamp(x, minX, maxX);
        //    var clampY = Mathf.Clamp(y, minY, maxY);
        //    transform.position = new Vector2(clampX, clampY);
        //}
 
 */