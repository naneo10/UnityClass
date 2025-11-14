using UnityEngine;
using UnityEngine.UI;

public class AbilityRunner : MonoBehaviour
{
    [SerializeField] private Ability m_CurrentAbility;
    [SerializeField] private GameObject owner;
    [SerializeField] private Button m_Button;

    public Ability CurrentAbility
    {
        get => m_CurrentAbility;
        set
        {
            m_CurrentAbility = value;
            UpdateButtonState();
        }
    }

    private void OnEnable()
    {
        if (m_Button != null)
        {
            m_Button.onClick.AddListener(OnAbilityButtonClicked);
        }
    }

    private void OnDisable()
    {
        if (m_Button !=  null)
        {
            m_Button.onClick.RemoveAllListeners();
        }
    }

    void Start()
    {
        UpdateButtonState();
    }

    public void OnAbilityButtonClicked()
    {
        if (m_CurrentAbility != null)
        {
            GameObject user;

            if (owner != null)
            {
                user = owner;
            }
            else
            {
                user = this.gameObject;
            }
            m_CurrentAbility.Use(user);
        }
    }

    private void UpdateButtonState()
    {
        bool hasAbility = m_CurrentAbility != null;

        m_Button.gameObject.SetActive(hasAbility);

        if(hasAbility)
        {
            m_Button.image.sprite = m_CurrentAbility.ButtonIcon;
        }
        else
        {
            m_Button.image.sprite = null;
        }
    }
}
