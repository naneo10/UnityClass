using UnityEngine;

public class CPlayerShooter : ShootBase
{
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Fire();
        }
    }
}
