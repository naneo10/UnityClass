using System.Collections.Generic;
using TMPro;
using UnityEngine;

//런타임에서 실시간으로 변경해주는게 포인트 : 전략

[System.Serializable] //한 쌍으로 묶는 데이터
public struct AbilityThreshold
{
    public Ability ability;
    public int minimumStreak;
}

public class StreakCounter : MonoBehaviour
{
    [SerializeField] private List<AbilityThreshold> m_AbilityThresholds; //리스트 관리
    [SerializeField] private AbilityRunner m_AbilityRunner; //선택된 어빌리티를 실행

    [SerializeField] private TextMeshProUGUI m_StreakText;

    private int m_CurrentStreak = 0; //갯수

    public int CurrentStreak
    {
        get => m_CurrentStreak;
        set
        {
            m_CurrentStreak = value;

            UpdateCurrentAbility();
            UpdateStreakText();
        }
    }

    void Start()
    {
        UpdateCurrentAbility();
        UpdateStreakText();
    }

    private void OnEnable()
    {
        GameEvents.OnCollectibleCollected += IncreamentStreak;
    }

    private void OnDisable()
    {
        GameEvents.OnCollectibleCollected -= IncreamentStreak;
    }

    private void UpdateStreakText()
    {
        if (m_StreakText != null)
        {
            m_StreakText.text = m_CurrentStreak.ToString();
        }
    }

    private void UpdateCurrentAbility()
    {
        if (m_AbilityRunner == null || m_AbilityThresholds == null || m_AbilityThresholds.Count == 0)
        {
            return;
        }

        Ability bestAbility = null;
        int bestMin = int.MinValue; //기준 값

        for (int i = 0; i < m_AbilityThresholds.Count; i++)
        {
            var th = m_AbilityThresholds[i];

            if (th.ability == null) continue;

            //현재 Streak=7
            //3->Fireball, 5->Iceball 10->rangeSkill
            if (th.minimumStreak <= m_CurrentStreak && th.minimumStreak > bestMin)
            {
                bestMin = th.minimumStreak;
                bestAbility = th.ability;
            }

            if (bestAbility != null)
            {
                m_AbilityRunner.CurrentAbility = bestAbility;
            }
        }
    }

    public void IncreamentStreak()
    {
        CurrentStreak++;
    }
}
