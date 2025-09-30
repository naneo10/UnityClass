using UnityEngine;

public class CItem : MonoBehaviour
{
    [SerializeField] private bool isDestory = false;
    public void Collect()
    {
        if(isDestory)
        {
            Destroy(gameObject);    
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
