using UnityEngine;

public class ScrollerBack : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform backTran;
    private float backSize;
    private float backPos;

    private void Start()
    {
        backTran = GetComponent<Transform>();
        backSize = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        backPos += speed * Time.deltaTime;
        backPos = Mathf.Repeat(backPos, backSize);
        backTran.position = new Vector2(0, -backPos);

    }
}
