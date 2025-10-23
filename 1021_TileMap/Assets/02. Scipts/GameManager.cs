using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int killCount { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCount()
    {
        killCount += 1;
        Debug.Log(killCount);

        if (killCount % 5 == 0 )
        {
            Tower tower = FindObjectOfType<Tower>();
            if (tower != null)
            {
                tower.Upgrade();
            }
        }
    }
}
