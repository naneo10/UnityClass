using UnityEngine;

public class MethodBasic : MonoBehaviour
{
    [SerializeField] private int baseHp = 100;
    [SerializeField] private int baseAttack = 10;

    void Start()
    {
        Print("안녕");
        Print("문자열만");

        int startHP = StartHP(baseHp, 20);

        PrintStatus("플레이어", baseHp, baseAttack);
    }

    void Update()
    {
        
    }

    void Print(object msg) //object : 최상위 클래스 전부다 받을 수 있다 하위 클래스 항목
    {
        Debug.Log(msg);
    }

    //시작 Hp를 계산하는 메서드
    //매개변수 2개(기본 HP, 보너스 hp)
    int StartHP(int BaseHP, int BonusHP)
    {
        int result = BaseHP + BonusHP;
        Debug.Log($"[startHp] : {BaseHP}+{BonusHP} = {result}");
        return result;
    }

    //현재 스탯을 콘솔에 출력
    void PrintStatus(string title, int hp, int attack)
    {
        Debug.Log($"---{title}---");
        Debug.Log($"---{hp}---");
        Debug.Log($"---{attack}---");
    }

    private bool IsDead(int hp)
    {
        return hp <= 0;
    }

    //데미지를 입고 줄어든 HP를 반환하는 메서드
    private int TakeDamage(int currentHP, int damage)
    {
        currentHP -= damage;

        if (currentHP < 0) currentHP = 0;

        return currentHP;
    }

    private int CalcComboDamage(int attack) //오버로딩
    {
        return attack * 2;
    }

    private int CalcComboDamage(int attack, int comboCount) //오버로딩
    {
        return attack * comboCount;
    }
}
