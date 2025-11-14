using System;
using UnityEngine;

/*
[옵저버 패턴]
-어떤 일이 일어났을 떄 그걸 관심있는 애들한테 자동으로 알려주는 시스템

[핵심개념]
- Subject : 이벤트를 발생시키는 주체
- Observer : 그 이벤트에 관심있는 객체들
- Subscribe / notify : 옵저버가 subject를 구독하고 subject는 이벤트가 발생했을때 알림을 보냄

- 게임에서는 런타임에 여러 다양한 상황이 발생할수 있음. 적을 파괴하거나 파워업을 수집하거나 목표를 달성한다면?
- 특정오브젝트가 직접적인 참조없이 다른 오브젝트에 알림을 줄수 있게 하는 메커니즘이 필요한 경우가 많고 이에 따라 불필요한 종속관계 발생할수 있음
- 관찰자 패턴은 일반적으로 이런 유형의 문제를 해결할때 사용. 일대다 종속관계를 사용해서 오브젝트가 통신하되 낮은 결합도를 유지하도록 할수 있음.

예)플레이어의 체력이 변하면 UI변경, 경고음, 체력이 0이면 사망처리가 되어야함.
만약 옵저버 패턴이 없다면 플레이어가 직접UI, 사운드, 게임매니저를 전부 찾아서 호출해야함.(의존성이 너무 높음 -> 플레이어가 모든걸 알고 있음)

장점
- 결합도가 낮음 : 서로 직접 연결 X
- 확장 쉬움 : 새 옵저버 추가만으로 기능확장가능
*/
public class Player : MonoBehaviour
{
    public event Action<int> OnHealthChanged; //옵저버들이 들을 이벤트

    private int hp = 100;

    public void TakeDamage(int amount)
    {
        hp -= amount;
        OnHealthChanged?.Invoke(hp);//옵저버들에게 알림
    }
}

//옵저버 1
public class HealthUI : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnEnable()
    {
        player.OnHealthChanged += UpdateBar;
    }
    private void OnDisable()
    {
        player.OnHealthChanged -= UpdateBar;

    }
    private void UpdateBar(int newHp)
    {
        Debug.Log($"UI 갱신 : 체력{newHp}");
    }
}

//옵저버 2
public class WaringSound : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnEnable()
    {
        player.OnHealthChanged += PlayWarning;
    }
    private void OnDisable()
    {
        player.OnHealthChanged -= PlayWarning;

    }
    private void PlayWarning(int newHp)
    {
        if (newHp < 30)
        {
            Debug.Log("경고음 재생");
        }
    }
}

//public class Ovserver : MonoBehaviour
//{
//    void Start()
//    {
        
//    }

//    void Update()
//    {
        
//    }
//}
