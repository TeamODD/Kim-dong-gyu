using Unity.VisualScripting;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject targetObject; // isTrigger�� �ٲ� ������Ʈ
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trigger(targetObject.GetComponent<DisappearingPlatform>());
        }
    }

    void trigger(DisappearingPlatform targetCollider)
    {
        if (targetCollider != null)
        {
            targetCollider.isShaking = true;
            targetCollider.isTriggered = true;
        }
    }
}
