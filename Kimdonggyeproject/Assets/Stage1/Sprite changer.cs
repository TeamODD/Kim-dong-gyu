using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러를 직접 지정하거나
    public Sprite origin;              // 바꾸고 싶은 새 스프라이트
    public Sprite one;
    public Sprite two, three;
    void Start()
    {
        // 혹시 인스펙터에서 안 넣었을 경우 자동으로 찾아줌
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 이 함수를 호출하면 스프라이트가 바뀝니다
    public void ChangeSprite(int n)
    {
        Sprite target = spriteRenderer.sprite;
        if (n == 0)
            target = origin;
        else if (n == 1)
            target = one;
        else if (n == 2)
            target = two;
        else if (n == 3)
            target = three;
        spriteRenderer.sprite = target;
    }
}