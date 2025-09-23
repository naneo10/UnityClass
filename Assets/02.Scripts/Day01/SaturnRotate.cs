using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturnRotate : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 1.0f * Time.deltaTime);
        transform.Rotate(transform.up * 70.0f * Time.deltaTime);
    }
}
