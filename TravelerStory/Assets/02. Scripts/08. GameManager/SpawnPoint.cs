using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    #region field
    public GameObject prefabToSpawn;
    #endregion

    #region method
    public GameObject SpawnObject()
    {
        if (prefabToSpawn == null) return null;
        return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
    #endregion
}
