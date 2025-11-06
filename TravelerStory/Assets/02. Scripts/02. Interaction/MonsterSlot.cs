using UnityEngine;

public class MonsterSlot : MonoBehaviour
{
    #region
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
}
