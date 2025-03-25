using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expValue = 10; // 경험치 값 (프리팹마다 다르게 설정 가능)

    void OnTriggerEnter2D(Collider2D collider)
    {
        // 플레이어와 충돌 시
        if (collider.CompareTag("Player")) // 플레이어 태그 확인
        {
            //ExpToPlayer(collider.gameObject);
            Destroy(gameObject); // 충돌 후 즉시 파괴
        }
    }

    // 플레이어에게 경험치 부여 메서드
    private void ExpToPlayer(GameObject player)
    {
        
    }
}
