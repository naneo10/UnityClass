using UnityEngine;

public class Coin : MonoBehaviour
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
