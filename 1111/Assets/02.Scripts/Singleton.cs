using UnityEngine;

/*
생명주기가 길다
게임시작 시 무조건 시작
심플
장점:단순 그 자체
단점:불필요한 메모리를 사용하게 될 수 있다.
*/
public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
