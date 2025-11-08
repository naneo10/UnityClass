using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region field
    public static GameManager Instance { get; private set; }

    [Header("스폰 포인트")]
    [SerializeField] SpawnPoint playerSpawnPoint;
    [SerializeField] SpawnPoint BlueSpawnPoint;
    [SerializeField] SpawnPoint TurquoSpawnPoint;
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
        SetupScene();
    }

    void Update()
    {
        
    }

    #region method
    public void SetupScene()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            
        }
    }

    public void SpawnEnemy()
    {
        if (InteractionManager.Instance.LastMonster == null ||
            InteractionManager.Instance.LastMonster.Count == 0)
        {
            Debug.Log("MonstersInRange에 몬스터가 없음");
            return;
        }

        var monster = InteractionManager.Instance.LastMonster[0];

        if (monster.monsterData.monsterName == "FrankBlue")
        {
            if (BlueSpawnPoint != null)
            {
                GameObject blueMonster = BlueSpawnPoint.SpawnObject();
            }
        }
        else if (monster.monsterData.monsterName == "RattlesTurquo")
        {
            if (TurquoSpawnPoint != null)
            {
                GameObject turquoMonster = TurquoSpawnPoint.SpawnObject();
            }
        }
    }
    #endregion
}
