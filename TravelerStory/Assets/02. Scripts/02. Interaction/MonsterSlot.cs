using UnityEngine;

public class MonsterSlot : MonoBehaviour
{
    #region
    [SerializeField] public MonsterData monsterData;

    private MonsterData _monsterData;

    public MonsterData MonsterData
    {
        get { return _monsterData; }
        set
        {
            _monsterData = value;
        }
    }
    #endregion

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    #region
    #endregion
}
