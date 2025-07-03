using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        transform.Translate(VirtualJoystick.Value * speed * Time.deltaTime);

        var x = transform.position.x;
        var y = transform.position.y;

        var clampX = Mathf.Clamp(x, minX, maxX);
        var clampY =  Mathf.Clamp(y, minY, maxY);
        transform.position = new Vector2(clampX, clampY);
    }
}
