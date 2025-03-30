using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);

        DontDestroyOnLoad(gameObject);
    }
}
