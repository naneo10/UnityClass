using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerA : MonoBehaviour
{
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI bestClearTimeText;
    //Enemy
    [SerializeField] private GameObject[] enemys;

    private float timePassed; //총 킬 카운트
    private bool isGameOver; //게임오버 여부

    void Start()
    {
        timePassed = 0.0f;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            Restart();
        }

        //지나간 시간
        timePassed += Time.deltaTime;
        TimeText.text = "Time : " + (int)timePassed;

    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void EndGame()
    {
        isGameOver = true;
        StartCoroutine(GameOverTextCo());

        float bestClearTime = PlayerPrefs.GetFloat("BestClearTime");

        if (enemys.Length == 0)
        {
            if(timePassed > bestClearTime)
            {
                bestClearTime = timePassed;
                PlayerPrefs.SetFloat("BestClearTime", bestClearTime);
            }
        }

        bestClearTimeText.text = "Best Clear Time : " + (int)bestClearTime;
    }

    IEnumerator GameOverTextCo()
    {
        if(!gameOverText.TryGetComponent(out TextMeshProUGUI text))
        {
            yield break;
        }
        if (enemys.Length > 0)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].SetActive(false);
            }
        }
        gameOverText.SetActive(true);
        
    }
}
