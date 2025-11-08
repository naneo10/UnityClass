using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    #region field
    public Image image;

    [SerializeField] public TextMeshProUGUI skillName;
    [SerializeField] public TextMeshProUGUI skillDamage;
    [SerializeField] public TextMeshProUGUI skillDebuff;
    #endregion

    void Awake()
    {
        skillName.raycastTarget = false;
        skillDamage.raycastTarget = false;
        skillDebuff.raycastTarget = false;
    }
}
