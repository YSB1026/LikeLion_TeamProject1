using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 감지! 전환할 씬 이름: stage2");
            SceneManager.LoadScene("stage2");
        }
    }
}
