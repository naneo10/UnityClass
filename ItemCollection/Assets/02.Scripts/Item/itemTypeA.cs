using UnityEngine;

public class ItemTypeA : Item
{
    public float coolTime = 2.0f;

    public override int point => 1;

    protected override void Awake()
    {
        base.Awake();
    }
}
