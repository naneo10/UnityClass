using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("SavePoint setting")]
    [SerializeField] private string checkPointId = "CP_01"; //세이브 포인트의 아이디
    [SerializeField] private Transform spawnPoint; //플레이어가 리스폰할 위치

    public string CheckPointId 
    { 
        get { return checkPointId; }
    }
    public Transform SpawnPoint 
    { 
        get { return spawnPoint; }
    }

    //플레이거가 세이브 포인트에 들어오면
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.Show(checkPointId);
        }
    }

    //플레이어가 세이브 존에서 나갔을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.Hide();
        }
    }
}
