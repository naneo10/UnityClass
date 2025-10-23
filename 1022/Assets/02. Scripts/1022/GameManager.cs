using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager Instance { get; private set; }

    [Header("Player/UI")]
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("checkPoint")]
    [SerializeField] private SavePoint[] checkPoints;

    [SerializeField] private string defaultPlayerName = "hong";
    [SerializeField] private int defaultLevel = 1;

    public int Score { get; private set; }
    public string LastCheckPointId { get; private set; }

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

    void Start()
    {
        if (SaveSystem.TryLoad(out var loaded))
        {
            Score = loaded.score;
            LastCheckPointId = loaded.lastCheckPointId;

            UpdateScoreUI();
            TelePortCheckPoint();
        }
        else
        {
            Score = 0;
            LastCheckPointId = null;

            UpdateScoreUI();
        }
    }

    //점수
    public void AddScore(int amount)
    {
        Score += amount;
        UpdateScoreUI();
    }

    //점수 UI갱신
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score : {Score}";
        }
    }

    //세이브 포인트에 도착했을때 호출되는 메서드
    //현재 플레이어의 정보
    public void SaveCheckPoint(string checkPointId)
    {
        LastCheckPointId = checkPointId;

        var data = new GameData
        {
            playerName = defaultPlayerName,
            level = defaultLevel,
            score = Score,
            lastCheckPointId = LastCheckPointId
        };
        SaveSystem.Save(data);
    }

    //체크포인트 ID에 해당하는 지점을 찾아서 플레이어를 그 위치로 이동
    private void TelePortCheckPoint()
    {
        if (string.IsNullOrEmpty(LastCheckPointId)) return;
        if (checkPoints == null || checkPoints.Length == 0) return;
        if (player == null) return;

        for (int i = 0; i < checkPoints.Length; i++)
        {
            var cp = checkPoints[i];
            if (cp == null) continue;
            if (cp.CheckPointId != LastCheckPointId) continue;

            if (cp.SpawnPoint != null) //스폰 포인트가 있으면 이동
            {
                player.position = cp.SpawnPoint.position;
            }
            else
            {
                player.position = cp.transform.position;
            }
            break;
        }
    }
}
