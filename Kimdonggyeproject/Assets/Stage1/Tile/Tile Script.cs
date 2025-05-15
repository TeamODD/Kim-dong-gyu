using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class DisappearingPlatform : MonoBehaviour
{
    public float fadeDuration = 5f; // ���������� �� �ɸ��� �ð�
    public string playerTag = "Player";
    public string objectID = "고유한 id값으로 설정해주세요."; // 유니크한 이름이나 번호
    private Renderer rend;
    private Collider col;
    private Color originalColor;
    public bool isTriggered = false;
    private float fadeTimer = 0f;
    public float shakeDuration = 5f;     // ��鸱 �� �ð� (��)
    public float shakeAmplitude = 0.5f;  // ��鸮�� �� (�¿� �Ÿ�)
    public float shakeFrequency = 5f;    // ��鸲 �ӵ� (�������� ����)
    private float elapsedTime = 0f;
    private Vector3 originalPosition;
    public bool isShaking = false;
    public Renderer MR_basic;
    public Renderer MR_changed;
    private Vector3 origin_pos;
    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        originalColor = rend.material.color;
        origin_pos = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag(playerTag) && gameObject.layer == 8)
        {
            isTriggered = true;
            isShaking = true;
        }
    }

    void FixedUpdate()
    {
        if (isTriggered || isShaking)
        {
            Debug.Log("오답임니다.");
            if (isShaking)
            {
                elapsedTime += Time.deltaTime;

                float xOffset = Mathf.Sin(elapsedTime * shakeFrequency * Mathf.PI * 2) * shakeAmplitude;
                transform.position = originalPosition + new Vector3(xOffset, 0, 0);

                if (elapsedTime >= shakeDuration)
                {
                    isShaking = false;
                    transform.position = originalPosition; // 원래 위치로 복구
                }
            }
            else if(isTriggered)
            {
                Vector3 pos = transform.position;
                pos.y -= 0.01f;
                transform.position = pos;
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0f, fadeTimer / fadeDuration);
                Color newColor = originalColor;
                newColor.a = alpha;
                rend.material.color = newColor;
                if (fadeTimer >= fadeDuration)
                {
                    gameObject.SetActive(false);
                    isTriggered = false;
                    isShaking = false;
                }
            }
        }
    }
    public void RestorePlatform()
    {
        gameObject.SetActive(true);
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        rend.material.color = originalColor;
        isTriggered = false;
        isShaking = false;
        if(!gameObject.CompareTag("Alpha"))
            col.enabled = true;
        fadeTimer = 0f; // 필요 시 타이머도 초기화
        elapsedTime = 0f;
        transform.position = origin_pos;
    }
}