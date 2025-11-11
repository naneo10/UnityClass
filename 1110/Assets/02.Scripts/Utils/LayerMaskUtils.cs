using UnityEngine;

public class LayerMaskUtils : MonoBehaviour
{
    public static bool IsInLayer(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }

    //오버로딩도 가능
    public static bool IsInLayer(Collider col, LayerMask mask)
    {
        return (mask.value & (1 << col.gameObject.layer)) != 0;
    }
}
