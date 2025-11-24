using UnityEngine;

public abstract class Item : MonoBehaviour, IPointable
{
    protected Renderer itemColor;
    protected Rigidbody rd;

    public abstract int point { get; }

    protected virtual void Awake()
    {
        itemColor = gameObject.GetComponent<Renderer>();
        rd = GetComponent<Rigidbody>();
    }

    public virtual int GetPoint(int currentPoint, int point)
    {
        return currentPoint += point;
    }

    public virtual void ChangeColor(int currentPoint)
    {
        //material color 변경 : https://dalbitdorong.tistory.com/9
        //var itemList = PoolManager.Instance.pools;

        //itemColor = null : 문제 해결 전까지 보류
        if (currentPoint > 20 / 2 && currentPoint < 20 - 1)
        {
            itemColor.sharedMaterial.color = Color.red;
        }

        if (currentPoint == 20 - 1)
        {
            itemColor.sharedMaterial.color = Color.green;
        }

        if (currentPoint == 20)
        {
            itemColor.sharedMaterial.color = Color.blue;
        }
    }
}