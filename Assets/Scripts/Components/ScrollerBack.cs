using UnityEngine;

public class ScrollerBack : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform backTran;
    private float backSize;

    private float startY;
    private float offset;
    private void Start()
    {
        backTran = GetComponent<Transform>();
        backSize = GetComponent<SpriteRenderer>().bounds.size.y;
        startY = backTran.position.y;
        offset = 0f;
    }

    private void Update()
    {
        Scroll();

    }

    private void Scroll()
    {
        offset += speed * Time.deltaTime;

        float yOffset = Mathf.Repeat(offset, backSize);

        backTran.position = new Vector2(
            backTran.position.x,
            startY - yOffset);
    }
}
