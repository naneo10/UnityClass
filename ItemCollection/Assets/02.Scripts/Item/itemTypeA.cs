using UnityEngine;

public class ItemTypeA : Item
{
    public float coolTime = 2.0f;

    public override int point => 1;

    protected override void Awake()
    {
        base.Awake();
    }

    public override int GetPoint(int currentPoint, int point)
    {
        PoolManager.Instance.ReturnPool(this);
        return base.GetPoint(currentPoint, point);
    }
}
