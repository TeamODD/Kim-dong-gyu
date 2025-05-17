using Unity.VisualScripting;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject targetObject; // isTrigger�� �ٲ� ������Ʈ
    public GameObject targetObject2; // isTrigger�� �ٲ� ������Ʈ
    public GameObject targetObject3; // isTrigger�� �ٲ� ������Ʈ
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(targetObject != null)
                trigger(targetObject.GetComponent<DisappearingPlatform>());
            if(targetObject2 != null)
                trigger(targetObject2.GetComponent<DisappearingPlatform>());
            if(targetObject3 != null)
                trigger(targetObject3.GetComponent<DisappearingPlatform>());
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
