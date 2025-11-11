using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region field
    public static GameManager Instance { get; private set; }
    public Transform Player;

    [Header("스폰 포인트")]
    [SerializeField] Transform PlayerSpawnPoint;
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //씬 전환 후 사용되지 않는 기능 잠금 I, K, N, P 등
        InteractionManager.Instance.changeScene = true;
        if (!InteractionManager.Instance.changeScene) return;
    }

    void Start()
    {
        SetupScene();
    }

    #region method
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) Player = playerObj.GetComponent<Transform>();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Player.position = PlayerSpawnPoint.position;
    }
    #endregion
}