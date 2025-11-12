using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus
{
    public static PlayerStatus instance;

    #region field
    public static PlayerStatus Instance()
    {
        if (instance == null)
        {
            instance = new PlayerStatus();
        }
        return instance;
    }

    public float hp = 300.0f;
    public float mp = 200.0f;
    public int damage = 20;
    public int skillDamage = 0;
    public int defense = 0;
    public int speed = 10;

    public float MaxHp = 300.0f;
    public float MaxMp = 200.0f;

    public int Gold = 1000;

    public PlayerBattle cPlayerBattle;
    #endregion

    public void Awake()
    {
        instance = this;
    }

    #region mathod
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject cPlayerBattle = GameObject.Find("PlayerBattle");

        if (cPlayerBattle != null) this.cPlayerBattle = cPlayerBattle.GetComponent<PlayerBattle>();
    }

    public void ModifyHP(float hp)
    {
        if (this.hp >= MaxHp)
        {
            Debug.Log("최대 체력이므로 먹을 수 없음");
            InteractionManager.Instance.useItem = false;
            if (cPlayerBattle != null)
            {
                cPlayerBattle.useItem = false;
            }
            return;
        }
        else if (this.hp < MaxHp)
        {
            InteractionManager.Instance.useItem = true;
            if (cPlayerBattle != null)
            {
                cPlayerBattle.useItem = true;
            }
            this.hp += hp;

            if (this.hp + hp >= MaxHp)
            {
                this.hp = 300.0f;
            }
        }
    }

    public void ModifyMP(float mp)
    {
        if (this.mp >= MaxMp)
        {
            Debug.Log("최대 마력이므로 먹을 수 없음");
            InteractionManager.Instance.useItem = false;
            if (cPlayerBattle != null)
            {
                cPlayerBattle.useItem = false;
            }
            return;
        }
        else if (this.mp < MaxMp)
        {
            InteractionManager.Instance.useItem = true;
            this.mp += mp;
            if (cPlayerBattle != null)
            {
                cPlayerBattle.useItem = true;
            }

            if (this.mp + mp >= MaxMp)
            {
                this.mp = 200.0f;
            }
        }
    }
    #endregion
}