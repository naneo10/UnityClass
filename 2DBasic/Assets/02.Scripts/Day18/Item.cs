using UnityEngine;

public class CItem : MonoBehaviour
{
    [SerializeField] private bool isDestroy = false;
    public void Collect()
    {
        if(isDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
