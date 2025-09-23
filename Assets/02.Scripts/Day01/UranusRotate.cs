using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranusRotate : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 0.4f * Time.deltaTime);
        transform.Rotate(transform.up * 20.0f * Time.deltaTime);
    }
}
