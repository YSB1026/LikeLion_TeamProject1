using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌 감지! 전환할 씬 이름: Stage1myj");
            SceneManager.LoadScene("Stage1myj");
        }
    }
}