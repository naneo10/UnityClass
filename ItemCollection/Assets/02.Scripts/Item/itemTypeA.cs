using UnityEngine;

public class ItemTypeA : Item
{
    public float coolTime = 2.0f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void GetPoint(int point)
    {
        base.GetPoint(point);
    }
}
