using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region field
    public static GameManager Instance { get; private set; }

    [Header("게임 오브젝트")]
    [SerializeField] public Transform player;
    [SerializeField] public Transform monster;

    [Header("스폰 포인트")]
    [SerializeField] private Transform spawnPoint;
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
    }

    void Start()
    {
        SpawnPoint();
    }

    void Update()
    {
        
    }

    #region method
    public void SpawnPoint()
    {
        player.position = spawnPoint.position;
    }
    #endregion
}
