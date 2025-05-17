using UnityEngine;

public class Colliding : MonoBehaviour
{
    public GameObject target;
    PlayerMovementRB playersc;
    VariableJump jumpsc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersc = target.GetComponent<PlayerMovementRB>();
        jumpsc = target.GetComponent<VariableJump>();
        if (playersc == null)
            Debug.LogWarning("PlayerMovementRB 스크립트를 찾을 수 없습니다.");
        if (jumpsc == null)
            Debug.LogWarning("VariableJump 스크립트를 찾을 수 없습니다.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 11) //투명 벽벽
            return;
        playersc.HandleCollision(collision);
        jumpsc.HandleCollision(collision);
    }
}
