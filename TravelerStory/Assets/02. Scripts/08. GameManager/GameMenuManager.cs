using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    #region field
    public static GameMenuManager Instance { get; private set; }

    private bool isOptionActive = false;
    #endregion

    void Awake()
    {
        if(Instance != null)
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
    public bool IsOptionActive
    {
        get { return isOptionActive; }
    }

    //람다식 1줄 작성 시 간결하게 작성할 수 있음
    public void OpenOption() => isOptionActive = true;
    public void CloseOtion() => isOptionActive = false;
    #endregion
}
