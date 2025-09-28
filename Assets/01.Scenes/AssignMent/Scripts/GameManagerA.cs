using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerA : MonoBehaviour
{
    [SerializeField] private GameObject gameWinText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI ClearTimeText;
    //Enemy
    [SerializeField] private GameObject[] enemies;

    private float timePassed; //시간체크
    private bool isGameWin; //승리 여부
    private bool isGameOver; //게임오버 여부

    private int totalEnemyCount = 20; //전체 적 수
    private int inactiveCount = 0; //꺼진 적 수

    private static GameManagerA instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timePassed = 0.0f;
        isGameWin = false;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            Restart();
        }
        else if (isGameWin)
        {
            Restart();
        }

        //지나간 시간
        timePassed += Time.deltaTime;
        timeText.text = "Time : " + (int)timePassed;
    }

    public static void EnemyDisabled()
    {
        instance.inactiveCount++;

        Debug.Log($"비활성화된 Enemy 수 {instance.inactiveCount}");

        if (instance.inactiveCount == instance.totalEnemyCount)
        {
            //CS0120 : https://itmining.tistory.com/128
            GameManagerA p = new GameManagerA();
            p.WinGame();
        }
    }
    
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("0925Assignment");
        }
    }

    public void LoseGame()
    {
        float ClearTime = PlayerPrefs.GetFloat("ClearTime");
        //Lose
        isGameOver = true;
        StartCoroutine(GameOverTextCo());

        ClearTimeText.text = "Clear Time : " + (int)ClearTime;
        
    }

    public void WinGame()
    {
        float ClearTime = PlayerPrefs.GetFloat("ClearTime");

        //Win
        isGameWin = true;

        ClearTime = timePassed;
        PlayerPrefs.SetFloat("ClearTime", ClearTime);
        ClearTimeText.text = "Clear Time : " + (int)ClearTime;
        StartCoroutine(GameWinTextCo());
    }

    IEnumerator GameOverTextCo()
    {
        if(!gameOverText.TryGetComponent(out TextMeshProUGUI text))
        {
            yield break;
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }
        gameOverText.SetActive(true);
    }

    IEnumerator GameWinTextCo()
    {
        if(!gameWinText.TryGetComponent(out TextMeshProUGUI text))
        {
            yield break;
        }
        gameWinText.SetActive(true);
    }
}
