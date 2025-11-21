using UnityEngine;

public abstract class Item : MonoBehaviour, IPointable
{
    public int currentPoint = 0;
    private int point = 1;

    protected Renderer itemColor;
    protected Rigidbody rd;

    protected virtual void Awake()
    {
        itemColor = GetComponent<Renderer>();
        rd = GetComponent<Rigidbody>();
    }

    public virtual void GetPoint(int point)
    {
        currentPoint += point;
        gameObject.SetActive(false);
        Debug.Log("GetPoint 실행 Item.cs");

        //material color 변경 : https://dalbitdorong.tistory.com/9
        var itemList = PoolManager.Instance.pools;

        if (currentPoint > itemList.Count / 2 && currentPoint < itemList.Count - 1)
        {
            itemColor.material.color = new Color(216.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        }

        if (currentPoint == itemList.Count - 1)
        {
            itemColor.material.color = Color.green;
        }

        if (currentPoint >= itemList.Count)
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