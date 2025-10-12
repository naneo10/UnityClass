using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region config
    //ÄÚÀÎ
    public int coin = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private TextMeshProUGUI clearTime;
    [SerializeField] private TextMeshProUGUI coinCount;
    [SerializeField] private GameObject clear;

    private float time;
    private bool gameClear;
    #endregion

    void Start()
    {
        time = 0.0f;
        gameClear = false;
    }

    void Update()
    {
        if(gameClear)
        {
            Restart();
        }

        time += Time.deltaTime;
        playTime.text = "Time : " + (int)time;

        coinCount.text = "" + coin;
    }

    #region Method
    public void IncreaseCoin()
    {
        coin += 1;
    }

    public void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void GameClear()
    {
        gameClear = true;
        GameClearText();

        clearTime.text = "ClearTime : " + (int)time;
    }

    public void GameClearText()
    {
        clear.SetActive(true);
    }
    #endregion
}
