using System;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    public event Action onGetCoin;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetCoin();
        }
    }

    public void GetCoin()
    {
        Debug.Log("플레이어가 코인을 얻음");
        onGetCoin?.Invoke();
    }
}
