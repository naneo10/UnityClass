using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "SO/MonsterData")]

public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int maxHp;
    public int maxMp;
    public int damage;
    public float speed;
    public GameObject prefab;
}
