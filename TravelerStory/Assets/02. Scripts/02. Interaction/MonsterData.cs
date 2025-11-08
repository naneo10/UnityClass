using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "SO/MonsterData")]

public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int hp;
    public int mp;
    public int damage;
    public float speed;
    public GameObject prefab;

    public int maxHp;
    public int maxMp;

    private void Awake()
    {
        
    }
}