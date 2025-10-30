using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    #region field
    public static UtilityManager Instance { get; private set; }
    #endregion

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }

    #region method
    public void TryLockCursor()
    {

    }

    public void UnlockCursor()
    {

    }
    #endregion
}
