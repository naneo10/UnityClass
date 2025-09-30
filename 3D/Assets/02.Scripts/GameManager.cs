using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //게임오버
    [SerializeField] private GameObject gameOverText;
    //생존시간 텍스트
    [SerializeField] private TextMeshProUGUI timeText;
    //최고기록 표시 텍스트
    [SerializeField] private TextMeshProUGUI scoreText;
    //총알 스포너
    [SerializeField] private GameObject[] bulletSpawn;

    private float surviveTime; //생존한 시간 
    private bool isGameOver; //게임오버 여부
    void Start()
    {
        surviveTime = 0.0f;
        isGameOver = false;
    }

    void Update()
    {
        //게임오버라면
        if(isGameOver)
        {
            //재시작
            Restart();
        }
        //생존시간 누적
        surviveTime += Time.deltaTime;
        //생존시간 표시
        timeText.text = "Time : " + (int)surviveTime;
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

        //최고기록 가져오기
        float bestTime = PlayerPrefs.GetFloat("BestTime");


        //생존시간이 최고 기록보다 크면
        if(surviveTime > bestTime)
        {
            //갱신해라(최고점수로)
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        scoreText.text = "Best Score : " + (int)bestTime;
    }

    IEnumerator GameOverTextCo()
    {
        //택스트 컴포넌트를 못찾으면 그냥 끝내라
        if(!gameOverText.TryGetComponent(out TextMeshProUGUI text))
        {
            yield break;
        }
        for (int i = 0; i < bulletSpawn.Length; i++)
        {
            //총알 스폰 오브젝트 비활성화
            bulletSpawn[i].SetActive(false);
        }
        //게임오버 텍스트 오브젝트 활성화
        gameOverText.SetActive(true);

        Color color = text.color;
        color.a = 0.0f;
        text.color = color;

        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * 2.0f;
            text.color = new Color(color.r, color.b, color.g, alpha);
            yield return null;
        }

        text.color = new Color(color.r, color.g, color.b, 1.0f);
    }

}
