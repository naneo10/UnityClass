using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "SO/MonsterData")]

public class MonsterData : ScriptableObject
{
    public string monsterName;
    public float hp;
    public float mp;
    public int damage;
    public float speed;
    public GameObject prefab;

    public float maxHp;
    public float maxMp;
}