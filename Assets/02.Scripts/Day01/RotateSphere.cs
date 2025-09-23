using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphere : MonoBehaviour
{

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 360.0f * Time.deltaTime); //공전
        transform.Rotate(transform.up * -360.0f * Time.deltaTime); //자전
    }
}
