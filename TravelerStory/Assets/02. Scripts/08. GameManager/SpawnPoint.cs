using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    #region field
    public GameObject prefabToSpawn;
    #endregion

    void Start()
    {
        //Invoke : https://chameleonstudio.tistory.com/37
        Invoke("SpawnObject", 2.0f);
    }

    #region method
    public GameObject SpawnObject()
    {
        if (prefabToSpawn == null) return null;
        return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
    #endregion
}
