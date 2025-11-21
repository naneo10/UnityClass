using UnityEngine;

public class DummyTarget : MonoBehaviour, IDamageble
{
    public void TakeDamage(int damage)
    {
        Debug.Log("이 녀석은 사라지지 않고 맞기만 한다");
    }
}
