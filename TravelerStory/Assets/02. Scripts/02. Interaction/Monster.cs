using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region field
    public static Monster Instance { get; private set; }

    [SerializeField] public GameObject[] monsters;
    [SerializeField] public MonsterSlot[] monsterSlots;
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region method

    #endregion
}
