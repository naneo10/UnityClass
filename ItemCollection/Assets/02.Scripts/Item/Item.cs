using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Item : MonoBehaviour, IPointable
{
    private ItemSpawner spawner;
    private int currentPoint = 0;
    private int point = 1;

    protected Renderer itemColor;
    protected Rigidbody rd;

    protected virtual void Awake()
    {
        itemColor = GetComponent<Renderer>();
        rd = GetComponent<Rigidbody>();
    }

    #region 외부 스크립트 자동으로 가져오기
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
        spawner = FindObjectOfType<ItemSpawner>(true);
    }
    #endregion

    public virtual void GetPoint(int point)
    {
        currentPoint += point;
        gameObject.SetActive(false);

        //material color 변경 : https://dalbitdorong.tistory.com/9
        if (currentPoint > spawner.itemTotalCount / 2 && currentPoint < spawner.itemTotalCount - 1)
        {
            itemColor.material.color = new Color(216.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        }

        if (currentPoint == spawner.itemTotalCount - 1)
        {
            itemColor.material.color = Color.green;
        }

        if (currentPoint >= spawner.itemTotalCount)
        {
            Clear();
        }
    }

    private void Clear()
    {
        //게임매니져에서 처리
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GetPoint(point);
        }
    }
}