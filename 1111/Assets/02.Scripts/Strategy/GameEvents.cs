using System;

public static class GameEvents
{
    public static event Action OnCollectibleCollected = delegate { }; //nullreference 오류 발생 방어

    public static void CollectibleCollected()
    {
        OnCollectibleCollected();
    }
}
